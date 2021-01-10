using Infraestrutura.QueryStore;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Queries.ContaCorrente.BuscarConta
{
    public sealed class BuscarContaQueryHandler : IRequestHandler<BuscarContaQueryRequest, BuscarContaQueryResult>
    {
        private readonly IContaCorrenteStore _contaCorrenteStore;

        public BuscarContaQueryHandler(IContaCorrenteStore contaCorrenteStore)
        {
            _contaCorrenteStore = contaCorrenteStore;
        }

        public async Task<BuscarContaQueryResult> Handle(BuscarContaQueryRequest request, CancellationToken cancellationToken)
        {   
            var retorno = await _contaCorrenteStore.BuscarContaCorrentePeloId(request.Id);

            if (retorno is null)
            {
                return new BuscarContaQueryResult();
            }

            return new BuscarContaQueryResult()
            {
                Agencia = retorno.Agencia,
                Banco = new BancoQueryResult()
                {
                    Id = retorno.Banco.bancoId,
                    Codigo = retorno.Banco.Codigo,
                    Nome = retorno.Banco.Nome
                },
                Id = retorno.Id,
                Numero = retorno.Numero
            };
            
        }
    }
}