namespace Domain.Transacoes
{
    public sealed class Banco
    {
        public string Nome { get; private set; }
        public int Codigo { get; private set; }

        public Banco(string nome, int codigo)
        {
            Nome = nome;
            Codigo = codigo;
        }
    }
}