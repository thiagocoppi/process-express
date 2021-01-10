using MediatR;
using System;

namespace Application.Commands.ContaCorrente.AtualizarDadosCadastrais
{
    public sealed class AtualizarDadosCadastraisCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Telefone { get; set; }
    }
}