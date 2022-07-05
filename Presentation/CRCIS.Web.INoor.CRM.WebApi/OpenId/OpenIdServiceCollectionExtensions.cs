using CRCIS.Web.INoor.CRM.Contract.Authentication;
using CRCIS.Web.INoor.CRM.Contract.Repositories.Users;
using CRCIS.Web.INoor.CRM.Contract.Security;
using CRCIS.Web.INoor.CRM.Contract.Service;
using CRCIS.Web.INoor.CRM.Contract.Settings;
using CRCIS.Web.INoor.CRM.Infrastructure.Authentication;
using CRCIS.Web.INoor.CRM.Infrastructure.Security;
using CRCIS.Web.INoor.CRM.Infrastructure.Settings;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.WebApi.OpenId
{
    public static class OpenIdServiceCollectionExtensions
    {
        ///// <summary>
        ///// Add JWT authentication scheme and config
        ///// </summary>
        ///// <param name="services">IServiceCollection</param>
        ///// <param name="appSettings">AppSettings</param>
        ///// <returns>IServiceCollection</returns>
        //public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, AppSettings appSettings)
        //{

        //    services.AddSingleton<ISecurityService, SecurityService>();
        //    services.AddScoped<IJwtProvider, JwtProvider>();
        //    services.AddScoped<ITokenValidator, TokenValidator>();
        //    services.AddScoped<ITokenStoreService, TokenStoreService>();

        //    IdentityModelEventSource.ShowPII = true; //Add this line
        //    services.AddAuthentication(options =>
        //    {
        //        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // "Bearer"
        //        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // "Bearer"
        //    }).AddJwtBearer(options =>
        //    {
        //        //options.Authority = "https://localhost:6001"; // Base-address of your identityserver
        //        //options.RequireHttpsMetadata = true;

        //        string authServerBaseUrl = appSettings.HostOptions?.AuthServer ?? "https://localhost:6001";
        //        bool isRequireHttpsMetadata = (!string.IsNullOrEmpty(authServerBaseUrl) && authServerBaseUrl.StartsWith("https")) ? true : false;
        //        options.Authority = string.IsNullOrEmpty(authServerBaseUrl) ? "https://localhost:6001" : authServerBaseUrl;
        //        options.RequireHttpsMetadata = isRequireHttpsMetadata;
        //        options.MetadataAddress = $"{authServerBaseUrl}/.well-known/openid-configuration"; // Optional
        //        options.Audience = appSettings?.AuthOptions?.Audience ?? string.Empty/* ApiResources.MyBackendApi2*/; // API Resource name
        //        options.TokenValidationParameters.ClockSkew = TimeSpan.Zero; // The JWT security token handler allows for 5 min clock skew in default
        //        options.BackchannelHttpHandler = AuthMetadataUtils.GetHttpHandler();
        //        //options.MetadataAddress = $"{authServerBaseUrl}/.well-known/openid-configuration";

        //        options.Events = new JwtBearerEvents()
        //        {
        //            OnAuthenticationFailed = (e) =>
        //            {
        //                // Some callback here ...
        //                return Task.CompletedTask;
        //            },
        //            OnTokenValidated = async context =>
        //            {
        //                var tokenValidatorService = context.HttpContext.RequestServices.GetRequiredService<ITokenValidator>();
        //                var adminRepository = context.HttpContext.RequestServices.GetRequiredService<IAdminRepository>();

        //                var inoorIdString = context.HttpContext.User.Claims.FirstOrDefault(a => a.Type == "sub")?.Value;

        //                if (Guid.TryParse(inoorIdString, out Guid inoorId))
        //                {
        //                    var admin = await adminRepository.FindAdminAsync(inoorId);

        //                    var adminId = admin?.Data?.Id;
        //                    if (adminId != null)
        //                    {
        //                        var claims = new List<Claim>
        //                        {
        //                            new Claim(ClaimTypes.UserData, adminId?.ToString()) ,
        //                            new Claim(ClaimTypes.Role, "Admin") ,
        //                        };

        //                        var appIdentity = new ClaimsIdentity(claims);
        //                        context.Principal.AddIdentity(appIdentity);
        //                    }

        //                }

        //            }
        //        };
        //    });

        //    return services;
        //}

        /// <summary>
        /// Add custom authentication
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        /// <param name="appSettings">AppSettings</param>
        /// <returns>Self</returns>
        public static IServiceCollection AddOpenIdAuthentication(this IServiceCollection services, AppSettings appSettings, IConfiguration configuration)
        {

            services.AddHttpContextAccessor();
            services.AddTransient<IIdentity>(sp =>
                sp.GetService<IHttpContextAccessor>().HttpContext.User.Identity);
            services.AddSingleton<ISecurityService, SecurityService>();


            IdentityModelEventSource.ShowPII = true;

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
             {
                 options.ExpireTimeSpan = TimeSpan.FromSeconds(30);
             })
            .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
            {
                const string CODE_VERIFIER_KEY = "code_verifier";
                const string CODE_CHALLENGE_KEY = "code_challenge";
                const string CODE_CHALLENGE_METHOD_KEY = "code_challenge_method";

                // Get config values from AppSetting file
                string oidcServerBaseUrl = appSettings?.HostOptions.AuthServer;
                bool isRequireHttpsMetadata = !string.IsNullOrEmpty(oidcServerBaseUrl) && oidcServerBaseUrl.StartsWith("https");
                options.Authority = oidcServerBaseUrl;// string.IsNullOrEmpty() ? "https://localhost:6001" : oidcServerBaseUrl;
                options.RequireHttpsMetadata = isRequireHttpsMetadata;
                options.MetadataAddress = $"{oidcServerBaseUrl}/.well-known/openid-configuration";
                options.BackchannelHttpHandler = AuthMetadataUtils.GetHttpHandler();

                options.ClientId = appSettings.AuthOptions.ClientId;
                options.ClientSecret = appSettings.AuthOptions.ClientSecret;
                options.ResponseType = "code";
                options.ResponseMode = "form_post";
                options.CallbackPath = $"/signin-oidc";
                options.SignedOutCallbackPath = $"/signout-callback-oidc";

                options.SaveTokens = true;
                options.Scope.Add("profile");
                options.Scope.Add("openid");

                options.Events.OnRedirectToIdentityProvider = context =>
                {
                 
                    context.ProtocolMessage.RedirectUri = $"{configuration["ApiUrl"]}signin-oidc";

                    // only modify requests to the authorization endpoint
                    if (context.ProtocolMessage.RequestType == OpenIdConnectRequestType.Authentication)
                    {
                        // generate code_verifier
                        var codeVerifier = CryptoRandom.CreateUniqueId(32);

                        //store codeVerifier for later use

                        context.Properties.Items.Remove(CODE_VERIFIER_KEY);
                        context.Properties.Items.Add(CODE_VERIFIER_KEY, codeVerifier);

                        //create code_challenge
                        string codeChallenge;
                        using (var sha256 = SHA256.Create())
                        {
                            var challengeBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(codeVerifier));
                            codeChallenge = Base64Url.Encode(challengeBytes);
                        }

                        //add code_challenge and code_challenge_method to request
                        context.ProtocolMessage.Parameters.Remove(CODE_CHALLENGE_KEY);
                        context.ProtocolMessage.Parameters.Remove(CODE_CHALLENGE_METHOD_KEY);
                        context.ProtocolMessage.Parameters.Add(CODE_CHALLENGE_KEY, codeChallenge);
                        context.ProtocolMessage.Parameters.Add(CODE_CHALLENGE_METHOD_KEY, "S256");
                    }

                    return Task.CompletedTask;
                };

                options.Events.OnAuthorizationCodeReceived = context =>
                {
                    // only when authorization code is being swapped for tokens
                    if (context.TokenEndpointRequest?.GrantType == OpenIdConnectGrantTypes.AuthorizationCode)
                    {
                        // get stored code_verifier
                        if (context.Properties.Items.TryGetValue(CODE_VERIFIER_KEY, out var codeVerifier))
                        {
                            // add code_verifier to token request
                            context.TokenEndpointRequest.Parameters.Add(CODE_VERIFIER_KEY, codeVerifier);
                        }
                    }

                    return Task.CompletedTask;
                };

                options.Events.OnUserInformationReceived = context =>
                {
                    return Task.CompletedTask;
                };

                options.Events.OnTokenValidated = ctc =>
                {
                    return Task.CompletedTask;
                };
            })
            ;

            return services;
        }
    }
}
