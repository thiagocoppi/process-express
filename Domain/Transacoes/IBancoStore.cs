using System.Threading.Tasks;

namespace Domain.Transacoes
{
    public interface IBancoStore
    {
        Task<Banco> RegistrarBanco(Banco banco);
        Task<Banco> BuscarBancoPeloCodigo(int codigoBanco);
    }
}