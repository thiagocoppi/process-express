using System;

namespace Application.Queries.ContaCorrente.BuscarConta
{
    public sealed class BuscarContaQueryResult
    {
        public Guid Id { get; set; }
        public long Numero { get; set; }
        public long Agencia { get; set; }
        public BancoQueryResult Banco { get; set; }
    }

    public sealed class BancoQueryResult
    {
        public Guid Id { get; set; }
        public int Codigo { get; set; }
        public string Nome { get; set; }
    }
}