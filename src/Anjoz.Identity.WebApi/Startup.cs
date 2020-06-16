using System;
using Anjoz.Identity.Application;
using Anjoz.Identity.Domain;
using Anjoz.Identity.Infrastructure;
using Anjoz.Identity.Repository;
using Anjoz.Identity.WebApi.Configuracoes;
using Anjoz.Package.Authentication.Domain;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BootstrapDomain = Anjoz.Identity.Domain.Bootstrap;

namespace Anjoz.Identity.WebApi
{
    public class Startup : IStartup
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _environment;

        private readonly string _policesDefaultAllowAll;

        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
            _policesDefaultAllowAll = Guid.NewGuid().ToString();
        }

        IServiceProvider IStartup.ConfigureServices(IServiceCollection services)
        {
            services.RegistrarSwagger();
            services.AddHttpContextAccessor();
            
            services.AddCors(options => options.AddPolicy(_policesDefaultAllowAll,
                builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()));
            services.AddMvc(opt => opt.RegisterFilterAnjozAuthorizantionGlobal())
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining(typeof(BootstrapDomain)));

            services
                .RegistrarApplicationServices()
                .RegistrarDomainServices(_configuration)
                .RegistrarInfrastructureServices(_configuration)
                .RegistrarRepositoryServices(_configuration);


            services.RegistrarAutenticacao(_configuration);
            services.AddAnjozAuthentication();

            return services.BuildServiceProvider();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseCors(_policesDefaultAllowAll);

            if (_environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            app.ConfigurarSwagger();
            app.UseGlobalExceptions(_environment, _policesDefaultAllowAll);
            app.UseHttpsRedirection();
            app.ConfigurarAutenticacao();
            app.UseMvc();
        }
    }
}
