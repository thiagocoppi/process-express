using Domain.ContaCorrentes;
using Domain.Transacoes;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.ContaCorrente.AtualizarDadosCadastrais
{
    public sealed class AtualizarDadosCadastraisCommandHandler : IRequestHandler<AtualizarDadosCadastraisCommand>
    {
        private readonly IContaService _contaService;

        public AtualizarDadosCadastraisCommandHandler(IContaService contaService)
        {
            _contaService = contaService;
        }

        public async Task<Unit> Handle(AtualizarDadosCadastraisCommand request, CancellationToken cancellationToken)
        {
            await _contaService.AtualizarDadosCadastrais(new Conta(request.Id, request.DataNascimento, request.Nome, request.Telefone));

            return new Unit();
        }
    }
}