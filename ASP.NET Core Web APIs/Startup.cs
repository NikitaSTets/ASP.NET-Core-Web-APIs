using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using ASP.NET_Core_Web_APIs.Constants;
using ASP.NET_Core_Web_APIs.Models;
using ASP.NET_Core_Web_APIs.Repositories;
using ASP.NET_Core_Web_APIs.Repositories.Interfaces;
using ASP.NET_Core_Web_APIs.Services.Interfaces;
using ASP.NET_Core_Web_APIs.Services.Validators;
using AutoWrapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace ASP.NET_Core_Web_APIs
{
    public class Startup
    {
        private readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


        public IConfiguration Configuration { get; }


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.WithOrigins("https://localhost:44321").WithMethods("GET");
                    });
            });

            services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    //options.SuppressInferBindingSourcesForParameters = true;
                    //options.SuppressMapClientErrors = true;
                    options.ClientErrorMapping[StatusCodes.Status400BadRequest].Link = "https://httpstatuses.com/400";
                })
                .AddXmlSerializerFormatters();

            services.AddResponseCaching(options =>
            {
                options.MaximumBodySize = 1024;
            });

            services.AddTransient<IMakeNameValidator, MakeNameValidator>();
            services.AddTransient<IRepository<Car>, CarsRepository>();
            services.AddTransient<IModelNameValidator, ModelNameValidator>();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(ApiVersions.V1, new OpenApiInfo
                {
                    Title = "Swagger Demo API",
                    Description = "Demo API for Swagger",
                    Version = ApiVersions.V1
                });

                options.SwaggerDoc(ApiVersions.V2, new OpenApiInfo
                {
                    Title = "Swagger Demo API 2",
                    Description = "Demo API for Swagger 2",
                    Version = ApiVersions.V2
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/echo",
                    context => context.Response.WriteAsync("echo"))
                    .RequireCors(MyAllowSpecificOrigins);

                endpoints.MapControllers()
                         .RequireCors(MyAllowSpecificOrigins);

                endpoints.MapGet("/echo2",
                    context => context.Response.WriteAsync("echo2"));
            });

            app.UseHttpsRedirection();

            app.UseApiResponseAndExceptionWrapper(new AutoWrapperOptions
            {
                UseCustomSchema = true
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            //app.UseSwagger(c =>
            //{
            //    c.RouteTemplate = "swagger/{documentname}/swagger.json";
            //});

            //app.UseSwaggerUI(c =>
            //{
            //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Cool API V1");
            //    c.RoutePrefix = "mycoolapi/swagger";
            //});

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "API V2");
            });
        }
    }
}