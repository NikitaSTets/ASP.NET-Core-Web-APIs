using System.ComponentModel.DataAnnotations;
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

namespace ASP.NET_Core_Web_APIs
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCaching();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseApiResponseAndExceptionWrapper(new AutoWrapperOptions()
            {
                UseCustomSchema = true
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}