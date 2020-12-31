using Domain.Tokens;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.Tokens.AbrirToken
{
    public sealed class AbrirTokenCommandHandler : IRequestHandler<AbrirTokenCommand, AbrirTokenCommandResult>
    {
        private readonly ITokenService _tokenService;

        public AbrirTokenCommandHandler(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public async Task<AbrirTokenCommandResult> Handle(AbrirTokenCommand request, CancellationToken cancellationToken)
        {
            var tokenInfo = await _tokenService.AbrirToken(new Token(request.Token));
            return new AbrirTokenCommandResult() { Data = tokenInfo.Data, Funcionario = tokenInfo.NomeFuncionario, Instituicao = tokenInfo.NomeInstituicao };
        }
    }
}