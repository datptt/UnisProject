using System;
using System.Security.Claims;
using Unis.Domain;

namespace Unis.API
{
    public interface ICalimsService
    {
        ClaimsPrincipal CreateClaimsPrincipal(User model);
    }
}
