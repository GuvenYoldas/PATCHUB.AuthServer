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
using PATCHUB.SharedLibrary.Dtos;
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
        public AuthenticationService(IOptions<List<Client>> optionsClient, TokenService tokenService, //UserManager<UserApp> userManager, IUnitOfWork unitOfWork, IGenericRepository<UserRefreshToken> userRefreshTokenService
            UserRepository userRepository, UserRefreshTokenRepository userRefreshTokenRepository)
        {
            _clients = optionsClient.Value;

            _tokenService = tokenService;
            //_userManager = userManager;
            //_unitOfWork = unitOfWork;
            //_userRefreshTokenService = userRefreshTokenService;

            _userRepository = userRepository;
            _userRefreshTokenRepository = userRefreshTokenRepository;
        }

        public async Task<Response<AppToken>> CreateTokenAsync(AppLogin login)
        {
      
            //var clientId = context.Request.Headers["Client-Id"].FirstOrDefault();
            //var clientSecret = context.Request.Headers["Client-Secret"].FirstOrDefault();

            if (login == null) throw new ArgumentNullException(nameof(login));

            var user = await _userRepository.GetFirstAsync(w => w.Mail == login.Email && w.StatusCode == (int)StatusCode.ACTIVE);

            if (user == null) return Response<AppToken>.Fail("Email or Password is wrong", 400, true);

            if (!Argon2.VerifyPassword(login.Password, user.PasswordHash, user.SaltString))
            {
                return Response<AppToken>.Fail("Email or Password is wrong", 400, true);
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
