using System.Collections.Generic;

namespace Anjoz.Identity.Domain.Entidades.Identity
{
    public class Claim
    {
        public Claim()
        {
            PerfisClaims = new HashSet<PerfilClaim>();
            UsuariosClaims = new HashSet<UsuarioClaim>();
        }

        public int Id { get; set; }

        public string Valor { get; set; }

        public string ValorNormalizado
        {
            get => Valor?.ToUpper();
            set { }
        }

        public string Descricao { get; set; }

        public string DescricaNormalizada
        {
            get => Descricao.ToUpperInvariant();
            set { }
        }

        public IEnumerable<PerfilClaim> PerfisClaims { get; set; }

        public IEnumerable<UsuarioClaim> UsuariosClaims { get; set; }
    }
}