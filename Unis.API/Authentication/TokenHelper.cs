using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Unis.Domain;

namespace Unis.API
{
    public static class Settings
    {
        public static string Secret = "marcy9d8534b48w951b9287d492b256x";
    }

    public static class UnisClaim
    {
        public static string UNIS_ROLE = "UNIS_ROLE";
        public static string UNIS_USERNAME = "UNIS_USERNAME";
        public static string UNIS_USERID = "UNIS_USERID";
        public static string UNIS_ISADMINISTRATOR = "UNIS_ISADMINISTRATOR";
    }

    public static class UnisRole
    {
        public static string UNIS_ROLE_USER = "UNIS_USER";
        public static string UNIS_ROLE_ADMIN = "UNIS_ADMIN";

    }


    public static class TokenHelper
    {
        public static string GetId(this ClaimsPrincipal principal)
        {
            var userIdClaim = principal.FindFirst(c => c.Type == UnisClaim.UNIS_USERID) ?? principal.FindFirst(c => c.Type == "sub");
            if (userIdClaim != null && !string.IsNullOrEmpty(userIdClaim.Value))
            {
                return userIdClaim.Value;
            }

            return null;
        }

        private const double EXPIRE_HOURS = 1.0;
        public static string CreateToken(User user)
        {
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            var tokenHandler = new JwtSecurityTokenHandler();
            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(UnisClaim.UNIS_USERNAME, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(UnisClaim.UNIS_ROLE, user.Role),
                    new Claim(UnisClaim.UNIS_USERID, user.Id.ToString()),
                    new Claim(UnisClaim.UNIS_ISADMINISTRATOR, user.Role.ToUpper().Equals(UnisRole.UNIS_ROLE_ADMIN) ? "true" : "false"),
                }),
                Expires = DateTime.UtcNow.AddHours(EXPIRE_HOURS),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(descriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
