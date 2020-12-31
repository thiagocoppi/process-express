using Domain.Base;
using System.Threading.Tasks;

namespace Domain.Tokens
{
    public interface ITokenService : IDomainService
    {
        Task<Token> GerarToken(Identificacao identificacao);
        Task<Identificacao> AbrirToken(Token token);
    }
}