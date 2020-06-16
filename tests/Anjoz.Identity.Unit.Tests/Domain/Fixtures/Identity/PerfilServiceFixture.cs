using System;
using System.Linq;
using System.Linq.Expressions;
using Anjoz.Identity.Domain.Contratos.Paginacao;
using Anjoz.Identity.Domain.Contratos.Repositorios.Identity;
using Anjoz.Identity.Domain.Contratos.Servicos.Identity;
using Anjoz.Identity.Domain.Entidades.Identity;
using Anjoz.Identity.Domain.Extensoes.Paginacao;
using Anjoz.Identity.Infrastructure.Extensoes;
using Anjoz.Identity.Utils.Tests.Fixture;
using Anjoz.Identity.Utils.Tests.Utils.Identity;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace Anjoz.Identity.Unit.Tests.Domain.Fixtures.Identity
{
    public class PerfilServiceFixture : BaseUnitTestFixture
    {
        public IPerfilRepository InicializarPerfilRepository()
        {
            var mock = new Mock<IPerfilRepository>();

            mock.Setup(lnq => lnq.CriarAsync(
                    It.Is<Perfil>(t => t.Name != PerfilUtils.NomePerfilInvalido)))
                .ReturnsAsync(IdentityResult.Success);

            mock.Setup(lnq => lnq.CriarAsync(
                    It.Is<Perfil>(t => t.Name == PerfilUtils.NomePerfilInvalido)))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError()));

            mock.Setup(lnq => lnq.AtualizarAsync(
                    It.Is<Perfil>(t => t.Name != PerfilUtils.NomePerfilInvalido)))
                .ReturnsAsync(IdentityResult.Success);

            mock.Setup(lnq => lnq.AtualizarAsync(
                    It.Is<Perfil>(t => t.Name == PerfilUtils.NomePerfilInvalido)))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError()));

            mock.Setup(lnq => lnq.ExcluirAsync(
                    It.Is<Perfil>(t => t.Name != PerfilUtils.NomePerfilInvalido)))
                .ReturnsAsync(IdentityResult.Success);

            mock.Setup(lnq => lnq.ExcluirAsync(
                    It.Is<Perfil>(t => t.Name == PerfilUtils.NomePerfilInvalido)))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError()));

            mock.Setup(lnq => lnq.ObterPorIdAsync(1))
                .ReturnsAsync(PerfilUtils.Perfis.First(lnq => lnq.Id == 1));

            mock.Setup(lnq => lnq.ObterPorIdAsync(9998))
                .ReturnsAsync(PerfilUtils.Perfis.First(lnq => lnq.Id == 9998));

            mock.Setup(lnq => lnq.ObterPorNomeAsync(
                    PerfilUtils.NomePerfilInvalido, It.IsAny<string[]>()))
                .ReturnsAsync(PerfilUtils.Perfis.First(lnq => lnq.NormalizedName == PerfilUtils.NomePerfilInvalido.ToUpper()));

            mock.Setup(lnq => lnq.ListarPorAsync(
                    It.IsAny<Expression<Func<Perfil, bool>>>(), It.IsAny<string[]>(), It.IsAny<IPagedParam>()))
                .ReturnsAsync((Expression<Func<Perfil, bool>> where, string[] includes, IPagedParam pagedParams) =>
                {
                    where ??= PredicateBuilderExtension.True<Perfil>();
                    return PerfilUtils.Perfis.Where(where.Compile()).ToPagedList();
                });

            return mock.Object;
        }

        public IPerfilClaimService InicializarPerfilClaimVinculoService()
        {
            var mock = new Mock<IPerfilClaimService>();

            mock.Setup(lnq => lnq.ListarTodosVinculosEntidade(
                    It.IsAny<int>(), It.Is<string[]>(t => t.Contains(nameof(PerfilClaim.Claim))),
                    It.IsAny<IPagedParam>()))
                .ReturnsAsync((int idPerfil, string[] includes, IPagedParam pagedParams) => PerfilClaimUtils.PerfilClaims.Where(lnq => lnq.RoleId == idPerfil).ToPagedList());
            
            return mock.Object;
        }
    }
}