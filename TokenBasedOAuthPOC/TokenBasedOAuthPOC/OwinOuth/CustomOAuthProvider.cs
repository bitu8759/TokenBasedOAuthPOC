using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;


namespace TokenBasedOAuthPOC.OwinOAuth
{
    public class CustomOAuthProvider: OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            if(context.UserName.ToLower() == "bnirala" && context.Password == "Nirala")
            {
                identity.AddClaim(new Claim(ClaimTypes.Name, "Bitu Kumar Nirala"));
                identity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
                identity.AddClaim(new Claim("username", context.UserName));
                context.Validated(identity);
            }
            else
            {
                context.SetError("Invalid  Grant", "User Name or Password is invalid");
                return;
            }
        }


    }
}