using Domain.Base;
using System.Threading.Tasks;

namespace Domain.Ofx
{
    public interface IOfxReader : IDomainService
    {
        Task RealizarImportacoes();
    }
}