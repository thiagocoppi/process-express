using System;

namespace Infraestrutura.Models.ContaCorrente
{
    public sealed class BancoDto
    {
        public Guid bancoId { get; set; }
        public string Nome { get; set; }
        public int Codigo { get; set; }
    }
}