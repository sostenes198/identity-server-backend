using System.Linq;
using Anjoz.Identity.Domain.Entidades.Identity;
using Anjoz.Identity.Infrastructure.Extensoes;
using Anjoz.Identity.Utils.Tests.Utils.Identity;
using FluentAssertions;
using Xunit;

namespace Anjoz.Identity.Unit.Tests.Infrastructure.Tests.Extensoes
{
    public class PredicateBuilderExtensionUnitTest
    {
        [Fact]
        public void Should_Create_True_Predicate()
        {
            var expression = PredicateBuilderExtension.True<Usuario>();
            var where = expression.Compile();

            var result = UsuarioUtils.Usuarios.Where(where);

            result.Should().NotBeNull().And.HaveCount(10);
        }
        
        [Fact]
        public void Should_Create_False_Predicate()
        {
            var expression = PredicateBuilderExtension.False<Usuario>();
            var where = expression.Compile();

            var result = UsuarioUtils.Usuarios.Where(where);

            result.Should().BeEmpty();
        }

        [Fact]
        public void Shoul_Create_Or_Predicate()
        {
            var expression = PredicateBuilderExtension.True<Usuario>();
            expression = expression.Or(lnq => lnq.Id == 1);
            var where = expression.Compile();
            
            var result = UsuarioUtils.Usuarios.Where(where);

            result.Should().NotBeNull().And.HaveCount(10);
        }
        
        [Fact]
        public void Shoul_Create_And_Predicate()
        {
            var expression = PredicateBuilderExtension.True<Usuario>();
            expression = expression.And(lnq => lnq.Id == 1);
            var where = expression.Compile();
            
            var result = UsuarioUtils.Usuarios.Where(where);

            result.Should().NotBeNull().And.HaveCount(1);
        }
    }
}