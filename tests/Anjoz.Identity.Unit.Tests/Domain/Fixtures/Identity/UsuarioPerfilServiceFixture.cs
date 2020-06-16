using System;
using System.Linq;
using System.Linq.Expressions;
using Anjoz.Identity.Domain.Contratos.Paginacao;
using Anjoz.Identity.Domain.Contratos.Repositorios.Identity;
using Anjoz.Identity.Domain.Contratos.Servicos.Identity;
using Anjoz.Identity.Domain.Entidades.Identity;
using Anjoz.Identity.Domain.Extensoes.Paginacao;
using Anjoz.Identity.Utils.Tests.Fixture;
using Anjoz.Identity.Utils.Tests.Utils.Identity;
using Moq;

namespace Anjoz.Identity.Unit.Tests.Domain.Fixtures.Identity
{
    public class UsuarioPerfilServiceFixture : BaseUnitTestFixture
    {
        public IUsuarioPerfilRepository InicializarUsuarioPerfilRepository()
        {
            var mock = new Mock<IUsuarioPerfilRepository>();

            mock.Setup(lnq => lnq.ListarPorAsync(
                    It.IsAny<Expression<Func<UsuarioPerfil, bool>>>(), It.IsAny<string[]>(), It.IsAny<IPagedParam>()))
                .ReturnsAsync((Expression<Func<UsuarioPerfil, bool>> where, string[] includes, IPagedParam pagedParams) 
                    => UsuarioPerfilUtils.UsuariosPerfis.Where(where.Compile()).ToPagedList());

            return mock.Object;
        }

        public IPerfilService InicializarPerfilSerivce()
        {
            var mock = new Mock<IPerfilService>();

            mock.Setup(lnq => lnq.ListarPorAsync(It.IsAny<Expression<Func<Perfil, bool>>>(),
                    It.IsAny<string[]>(), It.IsAny<IPagedParam>()))
                .ReturnsAsync((Expression<Func<Perfil, bool>> where, string[] includes, IPagedParam pagedParams) 
                    => PerfilUtils.Perfis.Where(where.Compile()).ToPagedList());

            return mock.Object;
        }
    }
}