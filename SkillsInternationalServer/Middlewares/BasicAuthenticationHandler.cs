using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using SkillsInternationalServer.Models;
using SkillsInternationalServer.Services;
using SkillsInternationalServer.Utilities;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace SkillsInternationalServer.Middlewares
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IAuthService _authService;

        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            System.Text.Encodings.Web.UrlEncoder encoder,
            ISystemClock clock,
            IAuthService authService)
            : base(options, logger, encoder, clock)
        {
            _authService = authService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("Missing Authorization Header");
            }

            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
                var username = credentials[0];
                var password = credentials[1];

                // Validate the user credentials with the database
                User? user = await _authService.LoginUser(username, password);

                if (user != null)
                {
                    var claims = new[] { new Claim(ClaimTypes.Name, user.Username) };
                    var identity = new ClaimsIdentity(claims, Scheme.Name);
                    var principal = new ClaimsPrincipal(identity);
                    var ticket = new AuthenticationTicket(principal, Scheme.Name);

                    return AuthenticateResult.Success(ticket);
                }
                else
                {
                    return AuthenticateResult.Fail(CreateUnauthorizedResponse());
                }
            }
            catch
            {
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }
        }


        private string CreateUnauthorizedResponse()
        {



            ApiResponse<int> response = new ApiResponse<int>
            {
                success = false,
                message = "Unauthorized access.",
                error = new Error { code = "AUTH001", message = "Please login and try again." }

               
            };

            return JsonSerializer.Serialize(response);
        }

        protected override Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            Response.StatusCode = StatusCodes.Status401Unauthorized;
            Response.ContentType = "application/json";

            var response = CreateUnauthorizedResponse();
            return Response.WriteAsync(response);
        }
    }


}
