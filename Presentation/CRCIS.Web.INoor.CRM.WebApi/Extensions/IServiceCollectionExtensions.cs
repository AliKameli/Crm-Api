using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.WebApi.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            //Define MapProfile Classes
            var mapperConfig = new MapperConfiguration(config =>
            {
                config.AddProfile<CRCIS.Web.INoor.CRM.Infrastructure.Mapping.ApplicationMapping>();
                config.AddProfile<CRCIS.Web.INoor.CRM.WebApi.Mapping.ApplicationMapping>();

            });

            mapperConfig.AssertConfigurationIsValid();
            services.AddSingleton(sp => mapperConfig.CreateMapper());

            return services;
        }   
    }
}
