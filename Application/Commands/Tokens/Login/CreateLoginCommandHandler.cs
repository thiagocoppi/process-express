using Domain.Tokens;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.Login
{
    public sealed class CreateLoginCommandHandler : IRequestHandler<CreateLoginCommand, CreateLoginCommandResult>
    {
        private readonly ITokenService _tokenService;

        public CreateLoginCommandHandler(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public async Task<CreateLoginCommandResult> Handle(CreateLoginCommand request, CancellationToken cancellationToken)
        {
            var tokenResult = await _tokenService.GerarToken(new Identificacao(request.Instituicao, DateTime.Now, request.Funcionario));
            return new CreateLoginCommandResult() { Token = tokenResult.Info };
        }
    }
}