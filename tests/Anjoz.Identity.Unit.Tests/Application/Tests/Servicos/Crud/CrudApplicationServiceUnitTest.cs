using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Anjoz.Identity.Application.Contratos.Crud;
using Anjoz.Identity.Application.Dtos.Base;
using Anjoz.Identity.Application.Dtos.Identity.Usuario;
using Anjoz.Identity.Application.Dtos.Paginacao;
using Anjoz.Identity.Application.Filtros.Identity;
using Anjoz.Identity.Application.Servicos.Crud;
using Anjoz.Identity.Domain.Entidades.Identity;
using Anjoz.Identity.Unit.Tests.Application.Fixture.Crud.Crud;
using FluentAssertions;
using Xunit;

namespace Anjoz.Identity.Unit.Tests.Application.Tests.Servicos.Crud
{
    public class CrudApplicationServiceUnitTest : IClassFixture<CrudApplicationServiceFixture>
    {
        private readonly ICrudApplicationService<UsuarioDto, int, Usuario, UsuarioFiltroDto, UsuarioCriarDto, UsuarioAtualizarDto> _applicationService;
        private readonly IEnumerable<UsuarioDto> _usuariosDto;

        public CrudApplicationServiceUnitTest(CrudApplicationServiceFixture fixture)
        {
            _applicationService = new CrudApplicationService<UsuarioDto, int, Usuario, UsuarioFiltroDto, UsuarioCriarDto, UsuarioAtualizarDto>(
                fixture.InicializarCrudService(), fixture.Mapper, new UsuarioGeradorFiltro());

            _usuariosDto = fixture.UsuariosDto;
        }

        [Fact]
        public async Task Deve_Obter_Por_Id()
        {
            var usuarioEsperado = _usuariosDto.SingleOrDefault(lnq => lnq.Id == 1);
            var usuario = await _applicationService.ObterPorIdAsync(1);

            usuario.Should().BeEquivalentTo(usuarioEsperado);
        }

        [Fact]
        public async Task Deve_Listar_Todos_Async()
        {
            var usuarios = await _applicationService.ListarPorAsync();
            var resultadoEsperado = new PagedListDto<UsuarioDto>(_usuariosDto.ToList(), 
                new PageInfoDto
                {
                    PageNumber = 1,
                    PageSize = 10,
                    TotalPages = 1
                });
            usuarios.Should().BeEquivalentTo(resultadoEsperado);
        }

        [Fact]
        public async Task Deve_Listar_Todos_Com_Filtro()
        {
            var filtro = new UsuarioFiltroDto {Id = 1};
            var usuariosEsperados = _usuariosDto.Where(lnq => lnq.Id == 1);
            var resultadoEsperado = new PagedListDto<UsuarioDto>(usuariosEsperados.ToList(), 
                new PageInfoDto
                {
                    PageNumber = 1,
                    PageSize = 1,
                    TotalPages = 1
                });

            var usuarios = await _applicationService.ListarPorAsync(filtro);

            usuarios.Should().BeEquivalentTo(resultadoEsperado);
        }

        [Fact]
        public async Task Deve_Criar()
        {
            var usuarioEsperado = new UsuarioDto()
            {
                Id = 1,
                Email = "teste@teste.com",
                Telefone = "9987411",
                Nome = "Teste"
            };


            var usuarioCriado = new UsuarioCriarDto
            {
                Email = "teste@teste.com",
                Senha = "testEa@3",
                Telefone = "9987411",
                Nome = "Teste"
            };

            var resultado = await _applicationService.CriarAsync(usuarioCriado);

            resultado.Should().BeEquivalentTo(usuarioEsperado);
        }

        [Fact]
        public async Task Deve_Atualizar()
        {
            var usuarioEsperado = _usuariosDto.Single(lnq => lnq.Id == 1);
            usuarioEsperado.Nome = "Teste Update";

            var usuarioAtualizar = new UsuarioAtualizarDto
            {
                Id = 1,
                CodigoEquipe = 1,
                Login = "teste1",
                Nome = "Teste Update",
                Email = "Teste@Email.com",
                Telefone = "987110"
            };

            var resultado = await _applicationService.AtualizarAsync(usuarioAtualizar);

            resultado.Should().BeEquivalentTo(usuarioEsperado, opt => opt
                .Excluding(prop => prop.Telefone)
                .Excluding(prop => prop.Email)
            );
        }

        [Fact]
        public void Deve_Deletar()
        {
            _applicationService.Invoking(async lnq => await lnq.ExcluirAsync(1))
                .Should().NotThrow();
        }
    }
}