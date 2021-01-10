using System;

namespace Infraestrutura.Models.ContaCorrente
{
    public sealed class ContaCorrenteDto
    {
        public Guid Id { get; set; }
        public BancoDto Banco { get; set; }
        public long Numero { get; set; }
        public long Agencia { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Contato { get; set; }
    }
}