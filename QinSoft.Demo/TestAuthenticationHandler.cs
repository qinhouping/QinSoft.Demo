using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Primitives;
using System.Security.Claims;

namespace QinSoft.Demo.Api
{
    public class TestAuthenticationHandler : IAuthenticationHandler
    {
        private HttpContext context;
        private AuthenticationScheme scheme;

        public async Task InitializeAsync(AuthenticationScheme scheme, HttpContext context)
        {
            this.scheme = scheme;
            this.context = context;
        }

        public async Task<AuthenticateResult> AuthenticateAsync()
        {
            if (context.Request.Headers.TryGetValue("Token", out StringValues strings))
            {
                ClaimsIdentity claimsIdentity = new ClaimsIdentity("simple");
                claimsIdentity.AddClaim(new Claim("Token", strings));
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
                ClaimsPrincipal user = new ClaimsPrincipal(claimsIdentity);
                return await Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(user, scheme.Name)));
            }
            else
            {
                return AuthenticateResult.Fail(new Exception("Token认证失败"));
            }
        }

        public async Task ChallengeAsync(AuthenticationProperties? properties)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await Task.CompletedTask;
        }

        public async Task ForbidAsync(AuthenticationProperties? properties)
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await Task.CompletedTask;
        }
    }
}
