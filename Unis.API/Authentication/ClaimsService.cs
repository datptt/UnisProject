using System;
using System.Security.Claims;
using Unis.Domain;

namespace Unis.API
{
    public sealed class ClaimsService : ICalimsService
    {
        public ClaimsPrincipal CreateClaimsPrincipal(User user)
        {
            if (user == null || user.Id == default) return default;

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var identity = new ClaimsIdentity(claims, "Bearer");

            var principal = new ClaimsPrincipal(identity);

            return principal;
        }
    }
}
