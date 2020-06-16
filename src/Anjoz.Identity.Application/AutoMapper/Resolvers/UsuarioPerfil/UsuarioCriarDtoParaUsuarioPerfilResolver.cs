using System.Collections.Generic;
using Anjoz.Identity.Application.Dtos.Identity.Usuario;
using Anjoz.Identity.Domain.Entidades.Identity;
using AutoMapper;

namespace Anjoz.Identity.Application.AutoMapper.Resolvers.UsuarioPerfil
{
    public class UsuarioAtualizarDtoParaUsuarioPerfilResolver : IValueResolver<UsuarioAtualizarDto, Usuario, IEnumerable<Domain.Entidades.Identity.UsuarioPerfil>>
    {
        public IEnumerable<Domain.Entidades.Identity.UsuarioPerfil> Resolve(UsuarioAtualizarDto source, Usuario destination, IEnumerable<Domain.Entidades.Identity.UsuarioPerfil> destMember, ResolutionContext context)
        {
            ICollection<Domain.Entidades.Identity.UsuarioPerfil> usuarioPerfis = new List<Domain.Entidades.Identity.UsuarioPerfil>();
            foreach (var roleId in source.PerfisId)
                usuarioPerfis.Add(new Domain.Entidades.Identity.UsuarioPerfil {RoleId = roleId});

            return usuarioPerfis;
        }
    }
}