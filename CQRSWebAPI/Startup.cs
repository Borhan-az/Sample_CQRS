using CQRSWebAPI.Behaviours;
using CQRSWebAPI.Caching;
using CQRSWebAPI.Filter;
using CQRSWebAPI.Model;
using CQRSWebAPI.Validation;
using MediatR;
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

namespace CQRSWebAPI
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

            services.AddControllers(opt => opt.Filters.Add(typeof(ResponseMappingFilter)));


            services.AddSingleton<SeedData>();
            services.AddMediatR(typeof(Startup).Assembly);
            services.AddMemoryCache();
            // All of our Validators
            services.AddValidator();
            services.AddTransient(typeof(IPipelineBehavior<,>),typeof(LoggingBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>),typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>),typeof(CachingBehaviour<,>));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CQRSWebAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CQRSWebAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
