using CRCIS.Web.INoor.CRM.Contract.Notifications;
using CRCIS.Web.INoor.CRM.Contract.Repositories.Logs;
using CRCIS.Web.INoor.CRM.Contract.Settings;
using CRCIS.Web.INoor.CRM.Infrastructure.Authentication.ClientIpCheck;
using CRCIS.Web.INoor.CRM.Infrastructure.Extensions;
using CRCIS.Web.INoor.CRM.Infrastructure.MailReader;
using CRCIS.Web.INoor.CRM.Infrastructure.Notifications;
using CRCIS.Web.INoor.CRM.Infrastructure.RabbitMq;
using CRCIS.Web.INoor.CRM.Infrastructure.Settings;
using CRCIS.Web.INoor.CRM.WebApi.Extensions;
using CRCIS.Web.INoor.CRM.WebApi.OpenId;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.WebApi
{
    public class Startup
    { 
        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            WebHostEnvironment = webHostEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment WebHostEnvironment { get; }

        private readonly string MyAllowSpecificOrigins = "Policy";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if (WebHostEnvironment.IsDevelopment() == false)
            {
                services.AddHostedService<ConsumerRabbitMQHostedService>();
                services.AddHostedService<TimedMailReaderHostedService>();
            }

            services.AddSingleton<IRabbitmqSettings>(sp =>
             Configuration.GetSection(nameof(RabbitmqSettings)).Get<RabbitmqSettings>());

            #region Identity Client
            services.AddSingleton<IIdentityClient, IdentityClient>();
            #endregion Identity Client
            var appSettings = Configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();
            #region HttpClient Factory
            services.AddHttpClient(HttpClientNameFactory.AuthHttpClient,
                config =>
                {
                    config.Timeout = TimeSpan.FromMinutes(5);
                    config.BaseAddress = new Uri(appSettings.HostOptions.AuthServer);
                    config.DefaultRequestHeaders.Add("Accept", "application/json");
                })
                .ConfigurePrimaryHttpMessageHandler(h =>
                {
                    var handler = new HttpClientHandler();

                    // Enable sending request to server with untrusted SSL cert 
                    handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
                    return handler;
                })
                .SetHandlerLifetime(TimeSpan.FromMinutes(5)); // HttpMessageHandler lifetime = 2 min

            // services.AddHttpClient<IIdentityClient, IdentityClient>().SetHandlerLifetime(TimeSpan.FromMinutes(2)) // HttpMessageHandler default lifetime = 2 min
            // .ConfigurePrimaryHttpMessageHandler(h =>
            // {
            //   var handler = new HttpClientHandler();
            //   if (this.env.IsDevelopment())
            //   {
            //       //Allow untrusted Https connection
            //       handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            //   }
            //   return handler;
            // });
            #endregion


            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for 
                //non -essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.Unspecified;
            });

            services.AddSingleton<IJwtSettings>(sp => Configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>());

            services.AddSingleton<IMailService, MailService>();
            services.AddSingleton<ISmsService, SmsService>();

            services.AddScoped<ICrmNotifyManager, CrmNotifyProvider>();

            services.AddControllers();
            services.AddMemoryCache();
            services.AddAutoMapper();
            services.AddDatabaseServices(Configuration);
            services.AddMasstransitServices(Configuration);
            services.AddDatabaseRepositoris();

            services.AddJwtAuthentication(Configuration);
            services.AddOpenIdAuthentication(appSettings, Configuration);
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "CRCIS.Web.INoor.CRM.WebApi", Version = "v1" });
                options.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
            });
            services.AddCors(options =>
            {

                options.AddPolicy(name: MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.WithOrigins("*")
                                            .AllowAnyHeader()
                                            .AllowAnyMethod();
                    });
                //options.AddPolicy(name: MyAllowSpecificOrigins,
                //    builder =>
                //    {
                //        builder.WithOrigins("http://localhost:8080")
                //                            .AllowAnyHeader()
                //                            .AllowAnyMethod();
                //    });
            });

            services.AddScoped<ClientIpCheckActionFilter>(container =>
            {
                var loggerFactory = container.GetRequiredService<ILoggerFactory>();
                var logger = loggerFactory.CreateLogger<ClientIpCheckActionFilter>();

                return new ClientIpCheckActionFilter(Configuration["AdminSafeList"], logger);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CRCIS.Web.INoor.CRM.WebApi v1"));
            }

            app.UseCookiePolicy();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("", context =>
                {
                    context.Response.Redirect($"{Configuration["VueUrl"]}", permanent: false);
                    return Task.FromResult(0);
                });
            });
        }
    }
}
