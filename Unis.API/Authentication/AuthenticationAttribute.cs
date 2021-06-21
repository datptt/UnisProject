using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Unis.API
{
    public class AuthenticationAttribute : Attribute, IPolicyEvaluator
    {
        public AuthenticationAttribute()
        {
        }

        public Task<AuthenticateResult> AuthenticateAsync(AuthorizationPolicy policy, HttpContext context)
        {
            var request = context.Request;
            var authorization = request.Headers["Authorization"];

            if (string.IsNullOrWhiteSpace(authorization))
            {
                return Task.FromResult(AuthenticateResult.Fail("No Authorization header found!"));
            }

            return Task.FromResult(AuthenticateResult.Fail("No Authorization header found!"));

        }

        public Task<PolicyAuthorizationResult> AuthorizeAsync(AuthorizationPolicy policy, AuthenticateResult authenticationResult, HttpContext context, object resource)
        {
            throw new NotImplementedException();
        }
    }
}
