using System;
using System.Linq;
using System.Linq.Expressions;
using Anjoz.Identity.Domain.Contratos.Paginacao;
using Anjoz.Identity.Domain.Contratos.Repositorios.Identity;
using Anjoz.Identity.Domain.Contratos.Servicos.Identity;
using Anjoz.Identity.Domain.Contratos.Validadores;
using Anjoz.Identity.Domain.Entidades.Identity;
using Anjoz.Identity.Domain.Excecoes;
using Anjoz.Identity.Domain.Extensoes.Paginacao;
using Anjoz.Identity.Domain.Servicos.Identity;
using Anjoz.Identity.Domain.VO;
using Anjoz.Identity.Infrastructure.Extensoes;
using Anjoz.Identity.Utils.Tests.Fixture;
using Anjoz.Identity.Utils.Tests.Utils.Identity;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace Anjoz.Identity.Unit.Tests.Domain.Fixtures.Identity
{
    public class UsuarioServiceFixture : BaseUnitTestFixture
    {
        public IUsuarioService InicializarUsuarioService(bool resultadoIdentity = true, bool saveValidator = true, bool updateValidator = true,
            bool usuarioAlteracaoSenhaCustomValidator = true, bool senhaValida = true)
        {
            return new UsuarioService(InicializarUsuarioRepository(resultadoIdentity), InicializarUsuarioClaimService(),
                InicializarUsuarioPerfilService(), DomainValidator(InicializarSaveValidator(saveValidator), InicializarUpdateValidator(updateValidator)),
                InicializarCustomValidatorUsuarioAlteracaoSenha(usuarioAlteracaoSenhaCustomValidator), InicializarSignInManagerService(senhaValida));
        }

        public IUsuarioRepository InicializarUsuarioRepository(bool resultadoIdentity = true)
        {
            var mock = new Mock<IUsuarioRepository>();

            mock.Setup(lnq => lnq.ObterPorIdAsync(It.IsAny<int>()))
                .ReturnsAsync((int id) => UsuarioUtils.Usuarios.SingleOrDefault(lnq => lnq.Id == id));

            mock.Setup(lnq => lnq.ObterPorNomeAsync(It.IsAny<string>(), It.IsAny<string[]>()))
                .ReturnsAsync((string name, string[] includes) => UsuarioUtils.Usuarios.SingleOrDefault(lnq => lnq.NormalizedUserName == name.ToUpper()));

            mock.Setup(lnq => lnq.ObterPorLoginAsync(It.IsAny<string>(), It.IsAny<string[]>()))
                .ReturnsAsync((string login, string[] includes) => UsuarioUtils.Usuarios.SingleOrDefault(lnq => lnq.LoginNormalizado == login.ToUpper()));

            mock.Setup(lnq => lnq.ListarPorAsync(
                    It.IsAny<Expression<Func<Usuario, bool>>>(), It.IsAny<string[]>(), It.IsAny<IPagedParam>()))
                .ReturnsAsync((Expression<Func<Usuario, bool>> where, string[] includes, IPagedParam pagedParams) =>
                {
                    where ??= PredicateBuilderExtension.True<Usuario>();
                    return UsuarioUtils.Usuarios.Where(where.Compile()).ToPagedList();
                });

            if (resultadoIdentity)
            {
                mock.Setup(lnq => lnq.CriarAsync(It.IsAny<Usuario>(), It.IsAny<string>()))
                    .ReturnsAsync(IdentityResult.Success);

                mock.Setup(lnq => lnq.AtualizarAsync(It.IsAny<Usuario>()))
                    .ReturnsAsync(IdentityResult.Success);

                mock.Setup(lnq => lnq.ExcluirAsync(It.IsAny<Usuario>()))
                    .ReturnsAsync(IdentityResult.Success);
            }
            else
            {
                mock.Setup(lnq => lnq.CriarAsync(It.IsAny<Usuario>(), It.IsAny<string>()))
                    .ReturnsAsync(IdentityResult.Failed(new IdentityError()));

                mock.Setup(lnq => lnq.AtualizarAsync(It.IsAny<Usuario>()))
                    .ReturnsAsync(IdentityResult.Failed(new IdentityError()));

                mock.Setup(lnq => lnq.ExcluirAsync(It.IsAny<Usuario>()))
                    .ReturnsAsync(IdentityResult.Failed(new IdentityError()));
            }

            return mock.Object;
        }

        public IUsuarioClaimService InicializarUsuarioClaimService()
        {
            var mock = new Mock<IUsuarioClaimService>();

            mock.Setup(lnq => lnq.ListarTodosVinculosEntidade(
                    It.IsAny<int>(),
                    It.Is<string[]>(t => t.Contains(nameof(UsuarioClaim.Claim))),
                    It.IsAny<IPagedParam>()))
                .ReturnsAsync((int idusuario, string[] includes, IPagedParam pagedParams)
                    => UsuarioClaimUtils.UsuariosClaims.Where(lnq => lnq.UserId == idusuario).ToPagedList());

            return mock.Object;
        }

        public IUsuarioPerfilService InicializarUsuarioPerfilService()
        {
            var mock = new Mock<IUsuarioPerfilService>();

            return mock.Object;
        }

        public ISaveServiceValidator<Usuario> InicializarSaveValidator(bool entidadeValida = true)
        {
            var mock = new Mock<ISaveServiceValidator<Usuario>>();

            if (entidadeValida)
                mock.Setup(lnq => lnq.ValidarEntidade(It.IsAny<Usuario>()));
            else
                mock.Setup(lnq => lnq.ValidarEntidade(It.IsAny<Usuario>()))
                    .Throws(new BusinessException("Error"));


            return mock.Object;
        }

        public IUpdateServiceValidator<Usuario> InicializarUpdateValidator(bool entidadeValida = true)
        {
            var mock = new Mock<IUpdateServiceValidator<Usuario>>();

            if (entidadeValida)
                mock.Setup(lnq => lnq.ValidarEntidade(It.IsAny<Usuario>()));
            else
                mock.Setup(lnq => lnq.ValidarEntidade(It.IsAny<Usuario>()))
                    .Throws(new BusinessException("Error"));


            return mock.Object;
        }

        public ICustomServiceValidator<UsuarioAlteracaoSenhaVo> InicializarCustomValidatorUsuarioAlteracaoSenha(bool entidadeValida = true)
        {
            var mock = new Mock<ICustomServiceValidator<UsuarioAlteracaoSenhaVo>>();

            if (entidadeValida)
                mock.Setup(lnq => lnq.ValidarEntidade(It.IsAny<UsuarioAlteracaoSenhaVo>()));
            else
                mock.Setup(lnq => lnq.ValidarEntidade(It.IsAny<UsuarioAlteracaoSenhaVo>()))
                    .Throws(new BusinessException("Error"));


            return mock.Object;
        }

        public ISignInManagerService InicializarSignInManagerService(bool senhaValida = true)
        {
            var mock = new Mock<ISignInManagerService>();

            mock.Setup(lnq => lnq.ChecarSenhaUsuario(It.IsAny<Usuario>(), It.IsAny<string>(), It.IsAny<bool>()))
                .ReturnsAsync((Usuario Usuario, string senha, bool travarAoFalhar) => senhaValida);

            return mock.Object;
        }
    }
}