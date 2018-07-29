using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;

[assembly: OwinStartup(typeof(TokenBasedOAuthPOC.OwinOAuth.Startup))]

namespace TokenBasedOAuthPOC.OwinOAuth
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            // Set Global.asax WebApiConfig registration
            GlobalConfiguration.Configure(WebApiConfig.Register);

            // Aloow Cors
            app.UseCors(CorsOptions.AllowAll);

            // Create instance of Custom OAuth Provider
            CustomOAuthProvider provider = new CustomOAuthProvider();

            OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions()
            {
                TokenEndpointPath = new PathString("/oauth/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(1),
                AllowInsecureHttp = true,
                Provider = provider
            };

            app.UseOAuthAuthorizationServer(options);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            // Configure Http
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);


        }
    }
}
