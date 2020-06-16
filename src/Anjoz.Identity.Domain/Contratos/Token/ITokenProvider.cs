using System.Threading.Tasks;

namespace Anjoz.Identity.Domain.Contratos.Token
{
    public interface ITokenProvider<T>
        where T : class
    {
        Task<string> GerarToken(T param);
    }
}