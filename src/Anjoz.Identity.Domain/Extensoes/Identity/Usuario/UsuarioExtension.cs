using System.Collections.Generic;
using Anjoz.Identity.Domain.Entidades.Identity;

namespace Anjoz.Identity.Domain.Extensoes.Identity.Usuario
{
    internal static class UsuarioExtension
    {
        public static void AtualizarValores(Entidades.Identity.Usuario usuario, Entidades.Identity.Usuario usuarioAplicacao)
        {
            usuarioAplicacao.UserName = usuario.UserName;
            usuarioAplicacao.Login = usuario.Login;
            usuarioAplicacao.Email = usuario.Email;
            usuarioAplicacao.PhoneNumber = usuario.PhoneNumber;
            usuarioAplicacao.CodigoEquipe = usuario.CodigoEquipe;
        }

        public static string ObterSenhaELimpar(Entidades.Identity.Usuario usuario)
        {
            var password = usuario.PasswordHash;
            usuario.PasswordHash = default;
            return password;
        }

        public static IEnumerable<UsuarioClaim> ObterUsuarioClaimELimpar(Entidades.Identity.Usuario usuario)
        {
            var usuarioClaims = usuario.UsuariosClaims;
            usuario.UsuariosClaims = default;
            return usuarioClaims;
        }

        public static IEnumerable<UsuarioPerfil> ObterUsuarioPerfilELimpar(Entidades.Identity.Usuario usuario)
        {
            var usuarioPerfis = usuario.UsuariosPerfis;
            usuario.UsuariosPerfis = default;
            return usuarioPerfis;
        }
    }
}