using System;

namespace Infraestrutura.Models.ContaCorrente
{
    public sealed class ContaCorrenteDto
    {
        public Guid Id { get; set; }
        public BancoDto Banco { get; set; }
        public long Numero { get; set; }
        public long Agencia { get; set; }
    }
}