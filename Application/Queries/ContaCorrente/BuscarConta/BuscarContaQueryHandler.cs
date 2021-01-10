using Infraestrutura.Models.ContaCorrente;
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
                Conta = new ContaCorrenteDto()
                {
                    Agencia = retorno.Agencia,
                    Banco = new BancoDto()
                    {
                        BancoId = retorno.Banco.BancoId,
                        Codigo = retorno.Banco.Codigo,
                        Nome = retorno.Banco.Nome
                    },
                    Contato = retorno.Contato,
                    DataNascimento = retorno.DataNascimento,
                    Nome = retorno.Nome,
                    Numero = retorno.Numero,
                    Id = retorno.Id
                }
            };
            
        }
    }
}