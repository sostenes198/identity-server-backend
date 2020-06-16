using System;
using System.Collections.Generic;
using System.Net.Http;
using Anjoz.Identity.Domain.Contratos.Servicos.Crud;
using Anjoz.Identity.Repository.EntityFramework.Context;
using Anjoz.Identity.Utils.Tests.Utils;
using Anjoz.Identity.WebApi;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Anjoz.Identity.Integration.Tests.Fixtures.Base
{
    public class BaseIntegrationTestFixture
    {
        public string Ambiente => "Development";
        public string BaseUrl => "api/v1";
        public Type Startup { get; set; } = typeof(Startup);
        public Action<WebHostBuilderContext, IServiceCollection> ConfigureServices { get; set; } = (context, services) => { };
        public Action<IApplicationBuilder> Configure { get; set; } = (builder) => { };

        public IServiceProvider ServiceProvider => TestServer.Host.Services;

        public IMapper Mapper => ServiceProvider.GetService<IMapper>();

        public HttpClient Client
        {
            get
            {
                var cliente = TestServer.CreateClient();
                cliente.DefaultRequestHeaders.Add("Authorization", $"Bearer {TokenUtils.Token}");
                return cliente;
            }
        }

        private TestServer TestServer => _testServer ?? (_testServer = new TestServer(CreateWebHost()));
        private TestServer _testServer;

        public BaseIntegrationTestFixture()
        {
            CriarSqLiteDb();
        }

        private IWebHostBuilder CreateWebHost()
        {
            IWebHostBuilder builder = new WebHostBuilder();
            builder.UseEnvironment(Ambiente);

            builder.ConfigureAppConfiguration((context, configurationBuilder) => configurationBuilder.AddJsonFile("appsettings.Development.json"));

            builder.ConfigureServices(ConfigureServices);
            builder.Configure(Configure);
            builder.UseStartup(Startup);

            return builder;
        }

        private void CriarSqLiteDb()
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                var scopeServiceProvider = scope.ServiceProvider;

                var db = scopeServiceProvider.GetRequiredService<IdentityContext>();
                db.Database.EnsureCreated();
            }
        }
        
        public Action CriarRegistros<TEntidade, TId>(ICollection<TEntidade> entidades, ICrudService<TEntidade, TId> crudService)
            where TEntidade : class
        {
            void Action() => GerarRegistros(entidades, crudService);
            return Action;
        }

        public Action CriarRegistros<TEntidade, TId>(ICollection<TEntidade> entidades, IWriteService<TEntidade, TId> writeService)
            where TEntidade : class
        {
            void Action() => GerarRegistros(entidades, writeService);
            return Action;
        }

        public Action CriarRegistros<TEntidade, TId>(TEntidade entidade, ICrudService<TEntidade, TId> crudService)
            where TEntidade : class
        {
            void Action() => GerarRegistros(entidade, crudService);
            return Action;
        }

        public Action CriarRegistros<TEntidade, TId>(TEntidade entidade, IWriteService<TEntidade, TId> writeService)
            where TEntidade : class
        {
            void Action() => GerarRegistros(entidade, writeService);
            return Action;
        }

        public void GerarRegistros<TEntidade, TId>(ICollection<TEntidade> entidades, IWriteService<TEntidade, TId> writeService)
            where TEntidade : class
        {
            foreach (var entidade in entidades)
                writeService.CriarAsync(entidade).GetAwaiter().GetResult();
        }

        public void GerarRegistros<TEntidade, TId>(TEntidade entidade, IWriteService<TEntidade, TId> writeService)
            where TEntidade : class
        {
            writeService.CriarAsync(entidade).GetAwaiter().GetResult();
        }
    }
}