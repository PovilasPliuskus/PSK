using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace TaskManager.API.Sso
{
    public static class SignOnConfiguration
    {
        private const string AuthCookieName = "an_auth";
        
        public static IServiceCollection ConfigureSso(this IServiceCollection service, AuthenticationValues values)
        {
            service
                .AddAuthentication(options =>
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                    options.Cookie.Name = AuthCookieName;
                    options.Cookie.HttpOnly = true;
                })
                .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = values.KeyCloakAuthority;
                    options.RequireHttpsMetadata = false;

                    options.ClientId = values.ClientId;
                    options.ClientSecret = values.ClientSecret;

                    options.ResponseType = OpenIdConnectResponseType.Code;
                    
                    // Here, we can add/remove scopes in the future depending on the user data our API might need to access
                    options.Scope.Clear();
                    options.Scope.Add("openid");
                    options.Scope.Add("profile");
                    options.Scope.Add("email");

                    options.GetClaimsFromUserInfoEndpoint = true;
                    options.SaveTokens = true;

                    options.MetadataAddress = values.MetadataAddress;
                    options.Events.OnRedirectToIdentityProvider = async context =>
                    {
                        context.ProtocolMessage.IssuerAddress = values.IssuerAddress;
                        await Task.CompletedTask;
                    };
                });

            return service;
        }
    }
}

