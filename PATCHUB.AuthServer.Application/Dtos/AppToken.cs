using System;

namespace PATCHUB.AuthServer.Application.Dtos
{
    public class AppToken
    {
        public string AccessToken { get; set; }
        public DateTime AccessTokenExpiration { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
    }
}
