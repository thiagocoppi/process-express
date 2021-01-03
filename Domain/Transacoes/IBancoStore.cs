using Domain.Base;
using System.Threading.Tasks;

namespace Domain.Transacoes
{
    public interface IBancoStore : IStore
    {
        Task<Banco> RegistrarBanco(Banco banco);
        Task<Banco> BuscarBancoPeloCodigo(int codigoBanco);
    }
}