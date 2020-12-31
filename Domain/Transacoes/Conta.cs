namespace Domain.Transacoes
{
    public sealed class Conta
    {
        public Banco Banco { get; private set; }
        public long Numero { get; private set; }
        public int Agencia { get; private set; }

        public Conta(Banco banco, long numero, int agencia)
        {
            Banco = banco;
            Numero = numero;
            Agencia = agencia;
        }
    }
}