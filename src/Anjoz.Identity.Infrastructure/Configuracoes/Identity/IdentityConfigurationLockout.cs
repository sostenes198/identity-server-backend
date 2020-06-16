namespace Anjoz.Identity.Infrastructure.Configuracoes.Identity
{
    public class IdentityConfigurationLockout
    {
        public bool PermitirParaNovosUsuarios { get; set; }

        public int TentativaMaximaErrors { get; set; }
    }
}