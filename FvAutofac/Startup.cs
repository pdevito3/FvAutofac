using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sieve.Services;

namespace FvAutofac
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
            services.AddControllers();

            services.AddAutoMapper(typeof(Startup));

            services.AddMvc(
                op =>
                {
                    op.RespectBrowserAcceptHeader = true;
                    op.ReturnHttpNotAcceptable = true;
                })
            .AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));

        }

        //public void ConfigureContainer(ContainerBuilder builder)
        //{
        //    builder.Register(c => new MapperConfiguration(cfg =>
        //    {
        //        cfg.AddProfile(new WfProfile(c.Resolve<ILogger<WfProfile>>()));
        //    }).CreateMapper());

        //    builder.RegisterType<WfRepo>().As<IWfRepo>();
        //    builder.RegisterType<SieveProcessor>();

        //    builder.RegisterAssemblyTypes(typeof(Startup).GetType().Assembly)
        //        .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
        //        .AsImplementedInterfaces();
        //}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
