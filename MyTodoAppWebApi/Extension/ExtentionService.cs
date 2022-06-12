using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace MyTodoAppWebApi.Extension
{
    public static class ExtentionService
    {

        public static void ConfigureCors(this IServiceCollection services) {

            services.AddCors(options => {
                options.AddPolicy("CorsPolicy",
               builder => builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader()
               );
            });

          
        }


        //public static void ConfigureSwagger(this IServiceCollection services)
        //{

        //    services.AddSwaggerGen(c =>
        //    { c.SwaggerDoc("v2", new OpenApiInfo { Version = "v2", Title = " LuckyApi 2.0 API", Description = " Lucky ezekiel 2.0 API" }); });

        //}


    }
}
