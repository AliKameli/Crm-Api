using CRCIS.Web.INoor.CRM.Contract.Authentication;
using CRCIS.Web.INoor.CRM.Contract.Repositories;
using CRCIS.Web.INoor.CRM.Contract.Repositories.Answers;
using CRCIS.Web.INoor.CRM.Contract.Repositories.Cases;
using CRCIS.Web.INoor.CRM.Contract.Repositories.Sources;
using CRCIS.Web.INoor.CRM.Contract.Repositories.Users;
using CRCIS.Web.INoor.CRM.Contract.Security;
using CRCIS.Web.INoor.CRM.Contract.Settings;
using CRCIS.Web.INoor.CRM.Data.Database;
using CRCIS.Web.INoor.CRM.Infrastructure.Authentication;
using CRCIS.Web.INoor.CRM.Infrastructure.Repositories;
using CRCIS.Web.INoor.CRM.Infrastructure.Repositories.Answers;
using CRCIS.Web.INoor.CRM.Infrastructure.Repositories.Cases;
using CRCIS.Web.INoor.CRM.Infrastructure.Repositories.Sources;
using CRCIS.Web.INoor.CRM.Infrastructure.Repositories.Users;
using CRCIS.Web.INoor.CRM.Infrastructure.Security;
using CRCIS.Web.INoor.CRM.Infrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Infrastructure.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            //Configuration Dependencies :
            services.AddSingleton<IJwtSettings>(sp =>
                configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>());

            services.AddScoped<ISecurityService, SecurityService>();
            services.AddScoped<IJwtProvider, JwtProvider>();
            services.AddScoped<ITokenStoreService, TokenStoreService>();

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
            services.AddScoped<IAnswerMethodRepository, AnswerMethodRepository>();
            services.AddScoped<ICommonAnswerRepository, CommonAnswerRepository>();
            //Cases:
            services.AddScoped<ISubjectRepository, SubjectRepository>();
            services.AddScoped<ICaseStatusRepository, CaseStatusRepository>();
            services.AddScoped<IOperationTypeRepository, OperationTypeRepository>();
            services.AddScoped<IImportCaseRepository, ImportCaseRepository>();
            services.AddScoped<IPendingCaseRepository, PendingCaseRepository>();
            services.AddScoped<IArchiveCaseRepository, ArchiveCaseRepository>();
            //Users:
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<ITokenRepository, TokenRepository>();

            return services;
        }

        public static IServiceCollection AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddSingleton<ISqlServerSettings>(sp =>
             configuration.GetSection(nameof(SqlServerSettings)).Get<SqlServerSettings>());

            services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>();
            return services;
        }
    }
}
