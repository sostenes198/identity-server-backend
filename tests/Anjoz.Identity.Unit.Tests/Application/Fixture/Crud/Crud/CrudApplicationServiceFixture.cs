using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Anjoz.Identity.Application.Dtos.Identity.Usuario;
using Anjoz.Identity.Domain.Contratos.Paginacao;
using Anjoz.Identity.Domain.Contratos.Servicos.Crud;
using Anjoz.Identity.Domain.Entidades.Identity;
using Anjoz.Identity.Domain.Extensoes.Paginacao;
using Anjoz.Identity.Utils.Tests.Fixture;
using Anjoz.Identity.Utils.Tests.Utils.Identity;
using Moq;

namespace Anjoz.Identity.Unit.Tests.Application.Fixture.Crud.Crud
{
    public class CrudApplicationServiceFixture : BaseUnitTestFixture
    {
        public ICrudService<Usuario, int> InicializarCrudService()
        {
            var mock = new Mock<ICrudService<Usuario, int>>();

            mock.Setup(lnq => lnq.ObterPorIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => UsuarioUtils.Usuarios.FirstOrDefault(lnq => lnq.Id == id));

            mock.Setup(lnq => lnq.ListarPorAsync(It.IsAny<Expression<Func<Usuario, bool>>>(),
                    It.IsAny<string[]>(), It.IsAny<IPagedParam>()))
                .ReturnsAsync(UsuarioUtils.Usuarios.ToPagedList);

            mock.Setup(lnq => lnq.ListarPorAsync(It.IsAny<Expression<Func<Usuario, bool>>>(),
                    It.IsAny<string[]>(), It.IsAny<IPagedParam>()))
                .ReturnsAsync((Expression<Func<Usuario, bool>> where, string[] includes, IPagedParam pagedParams) => where != default ? UsuarioUtils.Usuarios.Where(where.Compile()).ToPagedList() : UsuarioUtils.Usuarios.ToPagedList());

            mock.Setup(lnq => lnq.CriarAsync(It.IsAny<Usuario>()))
                .Returns((Usuario user) =>
                {
                    user.Id = 1;
                    return Task.FromResult(user);
                });

            mock.Setup(lnq => lnq.AtualizarAsync(It.IsAny<Usuario>()))
                .Returns((Usuario user) => Task.FromResult(user));

            mock.Setup(lnq => lnq.ExcluirAsync(It.IsAny<int>()));

            return mock.Object;
        }

        public ICollection<UsuarioDto> UsuariosDto => new List<UsuarioDto>
        {
            new UsuarioDto {Id = 1, CodigoEquipe = 1, Email = "teste1@teste.com", Nome = "teste1", Login = "teste1", Telefone = "111111"},
            new UsuarioDto {Id = 2, CodigoEquipe = 1, Email = "teste2@teste.com", Nome = "teste2", Login = "teste2", Telefone = "222222"},
            new UsuarioDto {Id = 3, CodigoEquipe = 1, Email = "teste3@teste.com", Nome = "teste3", Login = "teste3", Telefone = "333333"},
            new UsuarioDto {Id = 4, CodigoEquipe = 2, Email = "teste4@teste.com", Nome = "teste4", Login = "teste4", Telefone = "444444"},
            new UsuarioDto {Id = 5, CodigoEquipe = 2, Email = "teste5@teste.com", Nome = "teste5", Login = "teste5", Telefone = "555555"},
            new UsuarioDto {Id = 6, CodigoEquipe = 2, Email = "teste6@teste.com", Nome = "teste6", Login = "teste6", Telefone = "666666"},
            new UsuarioDto {Id = 7, CodigoEquipe = 3, Email = "teste7@teste.com", Nome = "teste7", Login = "teste7", Telefone = "777777"},
            new UsuarioDto {Id = 8, CodigoEquipe = 3, Email = "teste8@teste.com", Nome = "teste8", Login = "teste8", Telefone = "888888"},
            new UsuarioDto {Id = 9, Email = "teste9@teste.com", Nome = "teste9", Login = "teste9", Telefone = "999999"},
            new UsuarioDto {Id = 10, Email = "teste10@teste.com", Nome = "teste10", Login = "teste10", Telefone = "101010"},
        };
    }
}