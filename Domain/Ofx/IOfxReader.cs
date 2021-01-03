using Domain.Base;
using Domain.Transacoes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Ofx
{
    public interface IOfxReader : IDomainService
    {
        Task<List<Transacao>> RealizarImportacoes();
    }
}