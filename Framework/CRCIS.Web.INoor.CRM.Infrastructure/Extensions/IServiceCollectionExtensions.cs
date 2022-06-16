using CRCIS.Web.INoor.CRM.Contract.Authentication;
using CRCIS.Web.INoor.CRM.Contract.Repositories.Answers;
using CRCIS.Web.INoor.CRM.Contract.Repositories.Cases;
using CRCIS.Web.INoor.CRM.Contract.Repositories.Logs;
using CRCIS.Web.INoor.CRM.Contract.Repositories.Reports;
using CRCIS.Web.INoor.CRM.Contract.Repositories.Sources;
using CRCIS.Web.INoor.CRM.Contract.Repositories.Users;
using CRCIS.Web.INoor.CRM.Contract.Security;
using CRCIS.Web.INoor.CRM.Contract.Service;
using CRCIS.Web.INoor.CRM.Contract.Settings;
using CRCIS.Web.INoor.CRM.Data.Database;
using CRCIS.Web.INoor.CRM.Infrastructure.Authentication;
using CRCIS.Web.INoor.CRM.Infrastructure.Repositories.Logs;
using CRCIS.Web.INoor.CRM.Infrastructure.Repositories.Answers;
using CRCIS.Web.INoor.CRM.Infrastructure.Repositories.Cases;
using CRCIS.Web.INoor.CRM.Infrastructure.Repositories.Reports;
using CRCIS.Web.INoor.CRM.Infrastructure.Repositories.Sources;
using CRCIS.Web.INoor.CRM.Infrastructure.Repositories.Users;
using CRCIS.Web.INoor.CRM.Infrastructure.Security;
using CRCIS.Web.INoor.CRM.Infrastructure.Service;
using CRCIS.Web.INoor.CRM.Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using CRCIS.Web.INoor.CRM.Infrastructure.LoggerProvider;
using MassTransit;
using MassTransit.RabbitMqTransport.Topology;
using CRCIS.Web.INoor.CRM.Infrastructure.Masstransit;
using GreenPipes;
using RabbitMQ.Client;
using CRCIS.Web.INoor.CRM.Infrastructure.Masstransit.Notifications;
//using CRCIS.Web.INoor.CRM.Infrastructure.Masstransit.UserReportSupports;
using CRCIS.Web.INoor.CRM.Contract.Repositories.Permissions.Menu;
using CRCIS.Web.INoor.CRM.Infrastructure.Repositories.Permissions.Menu;
using CRCIS.Web.INoor.CRM.Infrastructure.Repositories.Permissions.AdminAction;
using CRCIS.Web.INoor.CRM.Contract.Repositories.Permissions.AdminAction;
using CRCIS.Web.INoor.CRM.Infrastructure.Repositories.Permissions.Role;
using CRCIS.Web.INoor.CRM.Contract.Repositories.Permissions.Role;
using CRCIS.Web.INoor.CRM.Infrastructure.Repositories.Permissions.RoleAdmin;
using CRCIS.Web.INoor.CRM.Contract.Repositories.Permissions.RoleAdmin;
using CRCIS.Web.INoor.CRM.Contract.ElkSearch;
using CRCIS.Web.INoor.CRM.Infrastructure.ElkSearch;

namespace CRCIS.Web.INoor.CRM.Infrastructure.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            //Configuration Dependencies :
            services.AddSingleton<IJwtSettings>(sp =>
                configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>());


            services.AddSingleton<ISecurityService, SecurityService>();
            services.AddScoped<IJwtProvider, JwtProvider>();
            services.AddScoped<ITokenValidator, TokenValidator>();
            services.AddScoped<ITokenStoreService, TokenStoreService>();

            services.AddHttpContextAccessor();
            services.AddTransient<IIdentity>(sp =>
                sp.GetService<IHttpContextAccessor>().HttpContext.User.Identity);

            // Needed for jwt auth.
            services
                .AddAuthentication(options =>
                {
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(cfg =>
                {
                    var secretKey = Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]);
                    var encryptionKey = Encoding.UTF8.GetBytes(configuration["JwtSettings:EncryptKey"]);

                    var validationParameters = new TokenValidationParameters
                    {
                        ClockSkew = TimeSpan.Zero, // tolerance for the expiration date
                        RequireSignedTokens = true,

                        ValidateIssuerSigningKey = true, // verify signature to avoid tampering
                        IssuerSigningKey = new SymmetricSecurityKey(secretKey),

                        RequireExpirationTime = true,
                        ValidateLifetime = true, // validate the expiration

                        ValidateAudience = false, // TODO: change this to avoid forwarding attacks
                        ValidAudience = configuration["JwtSettings:Audience"], // site that consumes the token

                        ValidateIssuer = true, // TODO: change this to avoid forwarding attacks
                        ValidIssuer = configuration["JwtSettings:Issuer"], // site that makes the token

                        TokenDecryptionKey = new SymmetricSecurityKey(encryptionKey),
                    };

                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = validationParameters;
                    cfg.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(JwtBearerEvents));
                            logger.LogError("Authentication failed.", context.Exception);
                            return Task.CompletedTask;
                        },
                        OnTokenValidated = context =>
                        {
                            var tokenValidatorService = context.HttpContext.RequestServices.GetRequiredService<ITokenValidator>();
                            return tokenValidatorService.ValidateAsync(context);
                        },
                        OnMessageReceived = context =>
                        {
                            return Task.CompletedTask;
                        },
                        OnChallenge = context =>
                        {
                            var logger = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(JwtBearerEvents));
                            logger.LogError("OnChallenge error", context.Error, context.ErrorDescription);
                            return Task.CompletedTask;
                        }
                    };
                });

            return services;
        }

        public static IServiceCollection AddDatabaseRepositoris(this IServiceCollection services)
        {
            //Sources:
            services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ISourceConfigRepository, SourceConfigRepository>();
            services.AddScoped<ISourceTypeRepository, SourceTypeRepository>();
            //Answers:
            services.AddScoped<IPendingHistoryRepository, PendingHistoryRepository>();
            services.AddScoped<IAnswerMethodRepository, AnswerMethodRepository>();
            services.AddScoped<ICommonAnswerRepository, CommonAnswerRepository>();
            //Cases:
            services.AddScoped<ISubjectRepository, SubjectRepository>();
            services.AddScoped<ICaseStatusRepository, CaseStatusRepository>();
            services.AddScoped<IOperationTypeRepository, OperationTypeRepository>();
            services.AddScoped<IImportCaseRepository, ImportCaseRepository>();
            services.AddScoped<IAutomaticImportCaseRepository, AutomaticImportCaseRepository>();
            services.AddScoped<IPendingCaseRepository, PendingCaseRepository>();
            services.AddScoped<IArchiveCaseRepository, ArchiveCaseRepository>();
            services.AddScoped<ICaseHistoryRepository, CaseHistoryRepository>();
            services.AddScoped<IReportRepository, ReportRepository>();
            services.AddScoped<ICaseSubjectRepository, CaseSubjectRepository>();
            //Users:
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<ITokenRepository, TokenRepository>();
            services.AddScoped<IActionRepository, ActionRepository>();
            services.AddScoped<IAdminActionRepository, AdminActionRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRoleActionRepository, RoleActionRepository>();
            services.AddScoped<IRoleAdminRepository, RoleAdminRepository>();

            //BL Databases
            services.AddScoped<ICaseNewService, CaseNewService>();
            services.AddScoped<IPendingCaseService, PendingCaseService>();
            services.AddScoped<IArchiveCaseService, ArchiveCaseService>();
            services.AddScoped<ICaseSubjectService, CaseSubjectService>();
            services.AddScoped<IProductService, ProductService>();

            //BL Admin
            services.AddTransient<IAdminService, AdminService>();
            services.AddTransient<INoorlockCommentService, NoorlockCommentService>();
            services.AddTransient<IAdminVerifyTokenRepository, AdminVerifyTokenRepository>();
            services.AddTransient<IPermissionService, PermissionService>();

            //Elk
            services.AddSingleton<IUserSearch, UserSearch>();

            return services;
        }

        public static IServiceCollection AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddSingleton<ISqlServerSettings>(sp =>
             configuration.GetSection(nameof(SqlServerSettings)).Get<SqlServerSettings>());

            services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>();
            return services;
        }

        public static ILoggingBuilder AddInDbLogger(this ILoggingBuilder builder)
        {
            builder.Services.AddSingleton<ILoggerProvider, InDbLoggerProvider>();
            //Logs
            builder.Services.AddScoped<ILogRepository, LogRepository>();

            return builder;
        }

        public static IServiceCollection AddMasstransitServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IRabbitmqSettings>(sp =>
             configuration.GetSection(nameof(RabbitmqSettings)).Get<RabbitmqSettings>());

            IRabbitmqSettings rabbitmqSettings = configuration.GetSection(nameof(RabbitmqSettings)).Get<RabbitmqSettings>();

            services.AddScoped<NotificationConsumer>();
            services.AddMassTransit(x =>
            {
                x.AddConsumer<NotificationConsumer>();
                //x.AddConsumer<CaseSubjectConsumer>();
                //x.AddConsumer<UserReportSupportConsumer>();

                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
                {
                    config.UseHealthCheck(provider);

                    config.PublishTopology.BrokerTopologyOptions = PublishBrokerTopologyOptions.MaintainHierarchy;
                    config.Host(rabbitmqSettings.HostName, 5672, rabbitmqSettings.VirtualHost, h =>
                     {
                         h.Username(rabbitmqSettings.Username);
                         h.Password(rabbitmqSettings.Password);
                     });

                    config.ReceiveEndpoint(rabbitmqSettings.QueueNotification, oq =>
                    {
                        oq.Bind(rabbitmqSettings.ExchangeNotification, x =>
                        {
                            x.Durable = true;
                            x.AutoDelete = false;
                            x.ExchangeType = ExchangeType.Fanout;// "fanout"
                                                                 //x.RoutingKey = "8675309";
                        });
                        oq.UseMessageRetry(r => r.Interval(2, 100));
                        oq.ConfigureConsumer<NotificationConsumer>(provider);
                    });

                    //config.ReceiveEndpoint(rabbitmqSettings.QueueCaseSujectUpdate, oq =>
                    //{
                    //    oq.Bind(rabbitmqSettings.ExchangeCaseSujectUpdate, x =>
                    //    {
                    //        x.Durable = true;
                    //        x.AutoDelete = false;
                    //        x.ExchangeType = ExchangeType.Fanout;// "fanout"
                    //                                             //x.RoutingKey = "8675309";
                    //    });
                    //    oq.UseMessageRetry(r => r.Interval(2, 100));
                    //    oq.ConfigureConsumer<CaseSubjectConsumer>(provider);
                    //});

                    config.Publish<NotificationValueDataEntered>(x =>
                    {
                        x.Exclude = true; // do not create an exchange for this type
                    });
                    //config.Publish<CaseSubjectUpdated>(x =>
                    //{
                    //    x.Exclude = true; // do not create an exchange for this type
                    //});

                    //config.ReceiveEndpoint(rabbitmqSettings.QueueFeedback, oq =>
                    //{
                    //    oq.Bind(rabbitmqSettings.ExchangeFeedback, x =>
                    //    {
                    //        x.Durable = true;
                    //        x.AutoDelete = false;
                    //        x.ExchangeType = ExchangeType.Fanout;// "fanout"
                    //                                             //x.RoutingKey = "8675309";
                    //    });
                    //    oq.ConfigureConsumer<UserReportSupportConsumer>(provider);
                    //});


                    //config.ReceiveEndpoint($"{rabbitmqSettings.QueueFeedback}-logs", oq =>
                    //{
                    //    oq.Lazy = true;
                    //    oq.Bind(rabbitmqSettings.ExchangeFeedback, x =>
                    //    {
                    //        x.Durable = true;
                    //        x.AutoDelete = false;
                    //        x.ExchangeType = ExchangeType.Fanout;// "fanout"
                    //    });
                    //    //oq.Bind<UserReportSupportConsumer>();
                    //    //oq.ConfigureConsumer<UserReportSupportConsumer>(provider);
                    //});
                }));
            });

            services.AddMassTransitHostedService();
            return services;
        }
    }
}