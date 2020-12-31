using System;

namespace Application.Commands.Tokens.AbrirToken
{
    public sealed class AbrirTokenCommandResult
    {
        public string Funcionario { get; set; }
        public string Instituicao { get; set; }
        public DateTime Data { get; set; }
    }
}