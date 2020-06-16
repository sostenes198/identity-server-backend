using System;
using System.IO;
using Anjoz.Identity.Domain.Contratos.Validadores;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Profile = Anjoz.Identity.Application.AutoMapper.Profiles.Base.Profile;

namespace Anjoz.Identity.Utils.Tests.Fixture
{
    public class BaseUnitTestFixture
    {
        private IServiceProvider _serviceProvider;
        private IConfiguration _configuration;

        public Action<IServiceCollection> AddServices { get; set; }
        public Func<string> ConfigureAppSetingsFile { get; set; }
        public IServiceProvider ServiceProvider => _serviceProvider ??= CreateDiContainer();
        public IConfiguration Configuration => _configuration ??= CreateConfiguration();
        public IMapper Mapper => _serviceProvider?.GetService<IMapper>() ?? (_serviceProvider = CreateDiContainer()).GetService<IMapper>();

        public BaseUnitTestFixture()
        {
            AddServices = (services) => { };
            ConfigureAppSetingsFile = () => "appsettings.Tests.json";
        }

        public IDomainServiceValidator<TEntity> DomainValidator<TEntity>(ISaveServiceValidator<TEntity> saveService = default, 
            IUpdateServiceValidator<TEntity> updateService = default, IDeleteServiceValidator<TEntity> deleteService = default, ICustomServiceValidator<TEntity> customService = default)
            where TEntity : class
        {
            var mock = new Mock<IDomainServiceValidator<TEntity>>();

            mock.Setup(lnq => lnq.Salvar)
                .Returns(saveService);
            
            mock.Setup(lnq => lnq.Atualizar)
                .Returns(updateService);
            
            mock.Setup(lnq => lnq.Deletar)
                .Returns(deleteService);
            
            mock.Setup(lnq => lnq.Customizado)
                .Returns(customService);

            return mock.Object;
        }

        private IConfiguration CreateConfiguration() =>
            new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(ConfigureAppSetingsFile())
                .Build();


        private IServiceProvider CreateDiContainer()
        {
            IServiceCollection services = new ServiceCollection();

            ConfigureAutoMapper(services);
            ConfigureServices(services);

            return services.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            AddServices(services);
        }

        private void ConfigureAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Profile).Assembly);
        }
    }
}