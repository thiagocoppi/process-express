using Dapper;
using Domain.Transacoes;
using Infraestrutura.Context;
using System;
using System.Threading.Tasks;

namespace Infraestrutura.Stores
{
    public sealed class TransacaoStore : ITransacaoStore
    {
        private readonly IProcessExpressContext _processExpressContext;

        public TransacaoStore(IProcessExpressContext processExpressContext)
        {
            _processExpressContext = processExpressContext;
        }

        public Task SalvarTransacao(Transacao transacao)
        {
            
            throw new NotImplementedException();
        }
    }
}