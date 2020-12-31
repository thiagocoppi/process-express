using MediatR;

namespace Application.Commands.Tokens.AbrirToken
{
    public sealed class AbrirTokenCommand : IRequest<AbrirTokenCommandResult>
    {
        public string Token { get; set; }
    }
}