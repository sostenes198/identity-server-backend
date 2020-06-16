namespace Anjoz.Identity.Infrastructure.Configuracoes.Identity
{
    public class IdentityConfigurationPassword
    {
        public int QuantidadeCaracterMinimo { get; set; }

        public bool RequerDigito { get; set; }

        public bool RequerLetraMinuscula { get; set; }

        public bool RequerLetraMaiuscula { get; set; }

        public int QuantidadeCaracterUnico { get; set; }

        public bool RequererCaracterEspecial { get; set; }
    }
}