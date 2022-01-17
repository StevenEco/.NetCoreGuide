using HttpIdentity.Attributes;
using Microsoft.AspNetCore.Authentication;
using System.Diagnostics;

namespace HttpIdentity.Middlewares
{

    public static class BasicAuthentication
    {
        public static void UseBA(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<BasicAuthenticationMiddleware>();
        }
    }

    public class BasicAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        public BasicAuthenticationMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<BasicAuthenticationMiddleware>();
        }
        public async Task Invoke(HttpContext context, IAuthenticationService authticationService)
        {
            var endpoint = context.GetEndpoint();
            bool hasAtrribute = endpoint == null?true:endpoint.Metadata.Any(p => p is HttpBasicAuthorize);
            if(!hasAtrribute)
            {
                await _next(context);
            }
            var authticated = await authticationService.AuthenticateAsync(context, "Basic");
            _logger.LogInformation("Access status:{0}", authticated.Succeeded);
            Debug.WriteLine("Access status:{0}", authticated.Succeeded);
            if (!authticated.Succeeded)
            {
                await authticationService.ChallengeAsync(context, "Basic", new AuthenticationProperties()
                {

                });
                return;
            }
            await _next(context);
        }
    }
}
