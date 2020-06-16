using System.Linq;
using Anjoz.Identity.Application.Dtos.Identity.Perfil;
using Anjoz.Identity.Application.Filtros.Identity;
using Anjoz.Identity.Domain.Entidades.Identity;
using Anjoz.Identity.Infrastructure.Interfaces.Filter;
using Anjoz.Identity.Utils.Tests.Utils.Identity;
using FluentAssertions;
using Xunit;

namespace Anjoz.Identity.Unit.Tests.Application.Tests.Filtros.Identity
{
    public class PerfilGeradorFiltroUnitTest
    {
        private readonly IGeradorFiltro<Perfil, PerfilFiltroDto> _geradorFiltro;

        public PerfilGeradorFiltroUnitTest()
        {
            _geradorFiltro = new PerfilGeradorFiltro();
        }

        [Fact]
        public void Deve_Filtar_Perfil_Por_Id()
        {
            var resultadoEsperado = PerfilUtils.Perfis.First(lnq => lnq.Id == 1);
            var filtro = new PerfilFiltroDto {Id = 1,};

            var expressao = _geradorFiltro.Gerar(filtro);

            var resultado = PerfilUtils.Perfis.First(expressao.Compile());

            resultado.Should().BeEquivalentTo(resultadoEsperado);
        }

        [Fact]
        public void Deve_Filtar_Perfil_Por_Nome()
        {
            var resultadoEsperado = PerfilUtils.Perfis.Where(lnq => lnq.NormalizedName.Contains("CC"));
            var filtro = new PerfilFiltroDto {Nome = "CC"};

            var expressao = _geradorFiltro.Gerar(filtro);
            var resultado = PerfilUtils.Perfis.Where(expressao.Compile());
            
            resultado.Should().BeEquivalentTo(resultadoEsperado);
        }
    }
}