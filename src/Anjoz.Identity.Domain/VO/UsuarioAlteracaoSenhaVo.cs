namespace Anjoz.Identity.Domain.VO
{
    public class UsuarioAlteracaoSenhaVo
    {
        public int Id { get; set; }

        public string SenhaAtual { get; set; }

        public string NovaSenha { get; set; }

        public string ConfirmacaoSenha { get; set; }
    }
}