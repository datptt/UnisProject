using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Unis.Repository;
using System.Linq;

namespace Unis.API
{
    public sealed class BearerPolicyEvaluator : IPolicyEvaluator
    {
        private const string Scheme = "Bearer";

        private readonly IUserRepository _repository;

        private readonly ICalimsService _calimsService;

        public BearerPolicyEvaluator(IUserRepository repository, ICalimsService calimsService)
        {
            _repository = repository;
            _calimsService = calimsService;
        }

        public async Task<AuthenticateResult> AuthenticateAsync(AuthorizationPolicy _, HttpContext context)
        {
            string bearerToken =
                context.Request.Headers.GetBearerToken(); //get bearer token from extension method.
            int statusCode = 401;
            string messageError = "Invalid Token";
            var princ = await AuthenticateJwtToken(bearerToken, ref messageError, ref statusCode);
            if (princ == null)
            {
               return await Task.FromResult(AuthenticateResult.Fail("Invalid token"));
            }
            else
            {
                var ticket = new AuthenticationTicket((ClaimsPrincipal)princ, Scheme);
                var authenticateResult = AuthenticateResult.Success(ticket);
                return await Task.FromResult(authenticateResult);
            }
        }

        public Task<PolicyAuthorizationResult> AuthorizeAsync(AuthorizationPolicy _,
            AuthenticateResult authenticationResult, HttpContext context,
            object resource)
        {
            var authorizeResult = authenticationResult.Succeeded
                ? PolicyAuthorizationResult.Success()
                : PolicyAuthorizationResult.Challenge();

            return Task.FromResult(authorizeResult);
        }


        protected Task<IPrincipal> AuthenticateJwtToken(string token, ref string errorMsg, ref int statusCode)
        {
            var tokenData = new TokenData();

            if (ValidateToken(token,tokenData,ref errorMsg,ref statusCode))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,tokenData.UserName),
                    new Claim(ClaimTypes.NameIdentifier,tokenData.UserID),
                    new Claim("Token",token),
                    new Claim("RoleName",tokenData.RoleName),
                    new Claim("ID",tokenData.UserID)
                };

                var identity = new ClaimsIdentity(claims, "Jwt");
                IPrincipal user = new ClaimsPrincipal(identity);
                return Task.FromResult(user);
            }
            return Task.FromResult<IPrincipal>(null);
        }


        // Lay ve thong tin login
        protected ClaimsPrincipal GetClaimsPrincipal(string token, ref DateTime lifeTime)
        {
            try
            {
                var tokenHandle = new JwtSecurityTokenHandler();

                var jwtToken = tokenHandle.ReadToken(token) as JwtSecurityToken;

                if (jwtToken == null)
                {
                    return null;
                }

                lifeTime = jwtToken.ValidTo;

                if (lifeTime <= DateTime.UtcNow)
                {
                    return null;
                }


                var key = Encoding.ASCII.GetBytes(Settings.Secret);

                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                SecurityToken securityToken;

                var principal = tokenHandle.ValidateToken(token, validationParameters, out securityToken);

                return principal;
            }
            catch (Exception ex)
            {
                var a =ex.Message;
                return null;
            }
        }

        //check token hop le
        private bool ValidateToken(string token, TokenData tokenData, ref string errorMsg, ref int statusCode)
        {
            DateTime lifeTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

            var simplePrinciple = GetClaimsPrincipal(token,ref lifeTime);

            var identity = simplePrinciple?.Identities.FirstOrDefault() as ClaimsIdentity;

            if (identity == null)
            {
                return false;
            }

            if (!identity.IsAuthenticated)
            {
                return false;

            }

            return true;
        }
    }
}
