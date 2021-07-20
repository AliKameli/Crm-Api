using CRCIS.Web.INoor.CRM.Infrastructure.Extensions;
using CRCIS.Web.INoor.CRM.Infrastructure.RabbitMq;
using CRCIS.Web.INoor.CRM.WebApi.Extensions;
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
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        readonly string MyAllowSpecificOrigins = "Policy";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.AddHostedService<ConsumerRabbitMQHostedService>();
            services.AddAutoMapper();
            services.AddDatabaseServices(Configuration);
            services.AddDatabaseRepositoris();
            services.AddJwtAuthentication(Configuration);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CRCIS.Web.INoor.CRM.WebApi", Version = "v1" });
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
                //        builder.WithOrigins("http://172.16.22.77:9011")
                //                            .AllowAnyHeader()
                //                            .AllowAnyMethod();
                //    });
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

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
