namespace Anjoz.Identity.Infrastructure.Configuracoes.Identity
{
    public class IdentityConfiguration
    {
        public bool RequerConfirmacaoEmail { get; set; }

        public IdentityConfigurationPassword SenhaConfiguracao { get; set; }

        public IdentityConfigurationLockout LockoutConfiguracao { get; set; }
    }
}