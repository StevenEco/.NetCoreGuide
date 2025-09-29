using HttpIdentity.Seeds;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace HttpIdentity.Handlers
{
    public static class BasicAuthenticationScheme
    {
        public const string DefaultScheme = "Basic";
    }

    public class BasicAuthenticationOption : AuthenticationSchemeOptions
    {
        public string Realm { get; set; }
    }

    public class BasicAuthenticationHandler : AuthenticationHandler<BasicAuthenticationOption>
    {
        private readonly BasicAuthenticationOption authOptions;
        public BasicAuthenticationHandler(
            IOptionsMonitor<BasicAuthenticationOption> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
            authOptions = options.CurrentValue;
        }

        /// <summary>
        /// 认证
        /// </summary>
        /// <returns></returns>
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Missing Authorization Header");
            string username, password;
            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');
                username = credentials[0];
                password = credentials[1];
                var isValidUser = IsAuthorized(username, password);
                if (isValidUser == false)
                {
                    return AuthenticateResult.Fail("Invalid username or password");
                }
            }
            catch
            {
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }

            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier,username),
                new Claim(ClaimTypes.Name,username),
            };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return await Task.FromResult(AuthenticateResult.Success(ticket));
        }

        /// <summary>
        /// 质询
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            Response.Headers["WWW-Authenticate"] = $"Basic realm=\"{Options.Realm}\"";
            await base.HandleChallengeAsync(properties);
        }

        /// <summary>
        /// 认证失败
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        protected override async Task HandleForbiddenAsync(AuthenticationProperties properties)
        {
            await base.HandleForbiddenAsync(properties);
        }

        private bool IsAuthorized(string username, string password)
        {
            return TestUsers.GetBAUsers()
                .Any(
                    p =>
                    {
                        return (p.Username == username)&&(p.Password == password);
                    }
                );
        }
    }
}
