using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PATCHUB.AuthServer.Application.Dtos;
using PATCHUB.AuthServer.Domain.Enumeration;
using PATCHUB.AuthServer.Persistence.Configurations;
using PATCHUB.AuthServer.Persistence.Repositories;
using PATCHUB.SharedLibrary.Abstractions;
using PATCHUB.SharedLibrary.Dtos;
using PATCHUB.SharedLibrary.ErrorHandling.Exceptions;
using PATCHUB.SharedLibrary.ErrorHandling.Mappers;
using PATCHUB.SharedLibrary.Helpers;

namespace PATCHUB.AuthServer.Infrastructure.AuthTokenService
{
    public class AuthenticationService
    {
        private readonly List<Client> _clients;
        private readonly TokenService _tokenService;
        //private readonly UserManager<UserApp> _userManager;
        //private readonly IUnitOfWork _unitOfWork;
        //private readonly IGenericRepository<UserRefreshToken> _userRefreshTokenService;+

        private readonly UserRepository _userRepository;
        private readonly UserRefreshTokenRepository _userRefreshTokenRepository;
        private readonly ClientCredentialRepository _clientCredentialRepository;
        private readonly IClientCredentialAccessor _clientCredentialAccessor;

        public AuthenticationService(IOptions<List<Client>> optionsClient, TokenService tokenService, //UserManager<UserApp> userManager, IUnitOfWork unitOfWork, IGenericRepository<UserRefreshToken> userRefreshTokenService
            UserRepository userRepository, UserRefreshTokenRepository userRefreshTokenRepository, IClientCredentialAccessor clientCredentialAccessor)
        {
            _clients = optionsClient.Value;

            _tokenService = tokenService;
            //_userManager = userManager;
            //_unitOfWork = unitOfWork;
            //_userRefreshTokenService = userRefreshTokenService;

            _userRepository = userRepository;
            _userRefreshTokenRepository = userRefreshTokenRepository;

            _clientCredentialAccessor = clientCredentialAccessor;
        }

        public async Task<Response<AppToken>> CreateTokenAsync(AppLogin login)
        {
            if (string.IsNullOrEmpty(_clientCredentialAccessor.ClientId) || string.IsNullOrEmpty(_clientCredentialAccessor.ClientSecret)) throw new BadRequestException("Client Bilgileri boş!");

            var clientId = Guid.TryParse(_clientCredentialAccessor.ClientId, out var parsed) ? parsed : (Guid?)null;
            var clientSecret = _clientCredentialAccessor.ClientSecret;

            if(clientId == null) throw new AuthenticationException("Client veya Giriş Bilgileri hatalı!");


            var clientCredential = await _clientCredentialRepository.GetFirstAsync(w => w.IDClient == clientId && w.SecretHash == clientSecret && w.StatusCode == (int)StatusCode.ACTIVE && w.ExpirationDate > DateTime.UtcNow && w.RequestLimit > w.RequestCount);
            if (clientCredential == null) throw new AuthenticationException("Client veya Giriş Bilgileri hatalı!");

            
            if (login == null) throw new BadRequestException("Giriş Bilgileri boş!");

            var user = await _userRepository.GetFirstAsync(w => w.Mail == login.Email && w.StatusCode == (int)StatusCode.ACTIVE);

            if (user == null) throw new AuthenticationException("Client veya Giriş Bilgileri hatalı!");

            if (!Argon2.VerifyPassword(login.Password, user.PasswordHash, user.SaltString))
            {
                throw new AuthenticationException("Client veya Giriş Bilgileri hatalı!");
            }

            AppUser appUserData = new AppUser
            {
                Id = user.ID,
                AddressBill = user.AddressBill ?? "",
                AddressShipping = user.AddressShipping ?? "",
                AvatarUrl = user.AvatarUrl ?? "",
                Balance = user.Balance,
                CountryCode = user.CountryCode ?? "",
                IdentityNumber = user.IdentityNumber ?? "",
                LastName = user.LastName ?? "",
                Mail = user.Mail ?? "",
                Name = user.Name ?? "",
                PhoneNo = user.PhoneNo ?? "",
                ReferenceUser = user.ReferenceUser ?? "",
                UserName = user.UserName ?? "",
                UserType = user.UserType
            };

            var token = _tokenService.CreateToken(appUserData);

            var userRefreshToken = await _userRefreshTokenRepository.GetFirstAsync(x => x.IDUser == user.ID);

            if (userRefreshToken == null)
            {
                _userRefreshTokenRepository.Insert(new Domain.Entities.UserRefreshTokenEntity { IDUser = user.ID, Token = token.RefreshToken, ExpirationDate = token.RefreshTokenExpiration });
            }
            else
            {
                userRefreshToken.Token = token.RefreshToken;
                userRefreshToken.ExpirationDate = token.RefreshTokenExpiration;
                _userRefreshTokenRepository.Update(userRefreshToken);
            }


            return Response<AppToken>.Success(token, 200);
        }


        public Response<AppTokenClient> CreateTokenByClient(AppLoginClient clientLoginDto)
        {
            var client = _clients.SingleOrDefault(x => x.Id == clientLoginDto.ClientId && x.Secret == clientLoginDto.ClientSecret);

            if (client == null)
            {
                return Response<AppTokenClient>.Fail("ClientId or ClientSecret not found", 404, true);
            }

            var token = _tokenService.CreateTokenByClient(client);

            return Response<AppTokenClient>.Success(token, 200);
        }

        public async Task<Response<AppToken>> CreateTokenByRefreshToken(string refreshToken)
        {
            var existRefreshToken = await _userRefreshTokenRepository.GetFirstAsync(x => x.Token == refreshToken);

            if (existRefreshToken == null)
            {
                return Response<AppToken>.Fail("Refresh token not found", 404, true);
            }

            var user = _userRepository.GetById(existRefreshToken.IDUser);

            if (user == null)
            {
                return Response<AppToken>.Fail("User Id not found", 404, true);
            }

            AppUser appUserData = new AppUser
            {
                Id = user.ID,
                AddressBill = user.AddressBill ?? "",
                AddressShipping = user.AddressShipping ?? "",
                AvatarUrl = user.AvatarUrl ?? "",
                Balance = user.Balance,
                CountryCode = user.CountryCode ?? "",
                IdentityNumber = user.IdentityNumber ?? "",
                LastName = user.LastName ?? "",
                Mail = user.Mail ?? "",
                Name = user.Name ?? "",
                PhoneNo = user.PhoneNo ?? "",
                ReferenceUser = user.ReferenceUser ?? "",
                UserName = user.UserName ?? "",
                UserType = user.UserType
            };

            var tokenDto = _tokenService.CreateToken(appUserData);

            existRefreshToken.Token = tokenDto.RefreshToken;
            existRefreshToken.ExpirationDate = tokenDto.RefreshTokenExpiration;

            _userRefreshTokenRepository.Update(existRefreshToken);

            return Response<AppToken>.Success(tokenDto, 200);
        }

        public async Task<Response<EmptyDto>> RevokeRefreshToken(string refreshToken)
        {
            var existRefreshToken = await _userRefreshTokenRepository.GetFirstAsync(x => x.Token == refreshToken);

            if (existRefreshToken != null)
            {
                _userRefreshTokenRepository.Delete(existRefreshToken.IDUser);
            }

            return Response<EmptyDto>.Success(200);
        }
    }

}
