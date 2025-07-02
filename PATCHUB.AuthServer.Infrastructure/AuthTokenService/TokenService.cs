using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using PATCHUB.AuthServer.Application.Dtos;
using Microsoft.Extensions.Options;
using PATCHUB.SharedLibrary.Dtos;
using PATCHUB.SharedLibrary.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using PATCHUB.AuthServer.Persistence.Configurations;

namespace PATCHUB.AuthServer.Infrastructure.AuthTokenService
{
    public class TokenService
    {
        private readonly CustomTokenOption _tokenOption;
        public TokenService(IOptions<CustomTokenOption> options)
        {
            _tokenOption = options.Value;
        }

        private string CreateRefreshToken()

        {
            var numberByte = new Byte[32];

            using var rnd = RandomNumberGenerator.Create();
            rnd.GetBytes(numberByte);

            return Convert.ToBase64String(numberByte);
        }

        private IEnumerable<Claim> GetClaims(AppUser userApp, List<String> audiences)
        {
            var claims = new List<Claim> {
                new Claim("id", userApp.Id.ToString()),
                new Claim("mail", userApp.Mail),
                new Claim("identityNumber", userApp.IdentityNumber),
                new Claim("tokenGuid", Guid.NewGuid().ToString()),

                new Claim("name", userApp.Name ?? ""),
                new Claim("lastName", userApp.LastName ?? ""),
                new Claim("userName", userApp.UserName ?? ""),
                new Claim("countryCode", userApp.CountryCode ?? ""),
                new Claim("phoneNo", userApp.PhoneNo ?? ""),
                new Claim("addressBill", userApp.AddressBill ?? ""),
                new Claim("addressShipping", userApp.AddressShipping ?? ""),
                new Claim("avatarUrl", userApp.AvatarUrl ?? ""),
                new Claim("balance", userApp.Balance.ToString("N2") ?? "0,00"),
                new Claim("userType", userApp.UserType.ToString()),
                new Claim("referenceUser", userApp.ReferenceUser)
            
            };

            claims.AddRange(audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));

            return claims;
        }

        private IEnumerable<Claim> GetClaimsByClient(Client client)
        {
            var claims = new List<Claim>();
            claims.AddRange(client.Audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));

            new Claim("tokenGuid", Guid.NewGuid().ToString());
            new Claim("id", client.Id.ToString());

            return claims;
        }

        public AppToken CreateToken(AppUser userApp)
        {
            var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.AccessTokenExpiration);
            var refreshTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.RefreshTokenExpiration);
            var securityKey = SignService.GetSymmetricSecurityKey(_tokenOption.SecurityKey);

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: _tokenOption.Issuer,
                expires: accessTokenExpiration,
                 notBefore: DateTime.Now,
                 claims: GetClaims(userApp, _tokenOption.Audience),
                 signingCredentials: signingCredentials);

            var handler = new JwtSecurityTokenHandler();

            var token = handler.WriteToken(jwtSecurityToken);

            var tokenDto = new AppToken
            {
                AccessToken = token,
                RefreshToken = CreateRefreshToken(),
                AccessTokenExpiration = accessTokenExpiration,
                RefreshTokenExpiration = refreshTokenExpiration
            };

            return tokenDto;
        }

        public AppTokenClient CreateTokenByClient(Client client)
        {
            var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.AccessTokenExpiration);

            var securityKey = SignService.GetSymmetricSecurityKey(_tokenOption.SecurityKey);

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: _tokenOption.Issuer,
                expires: accessTokenExpiration,
                 notBefore: DateTime.Now,
                 claims: GetClaimsByClient(client),
                 signingCredentials: signingCredentials);

            var handler = new JwtSecurityTokenHandler();

            var token = handler.WriteToken(jwtSecurityToken);

            var tokenDto = new AppTokenClient
            {
                AccessToken = token,

                AccessTokenExpiration = accessTokenExpiration,
            };

            return tokenDto;
        }
    }
}
