using System;
using System.Diagnostics.CodeAnalysis;
using Anjoz.Identity.Repository.EntityFramework.Context;
using Anjoz.Identity.Repository.EntityFramework.Repositorios.Identity;
using Anjoz.Identity.Unit.Tests.Repository.Fixtures.Crud;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Anjoz.Identity.Unit.Tests.Repository.Tests.EntityFramework.Repositorios.Identity
{
    public class UsuarioClaimRepositoryUnitTest : IClassFixture<CrudUnitTestRepositoryFixture>
    {
        private readonly CrudUnitTestRepositoryFixture _fixture;

        public UsuarioClaimRepositoryUnitTest(CrudUnitTestRepositoryFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        [SuppressMessage("ReSharper", "ObjectCreationAsStatement")]
        public void Deve_Inicializar_Repositorio()
        {
            Action act = () => new UsuarioClaimRepository(_fixture.ServiceProvider.GetService<IdentityContext>());
            act.Should().NotThrow();
        }
    }
}