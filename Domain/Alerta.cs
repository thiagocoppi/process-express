namespace Domain
{
    public sealed class Alerta
    {
        public string Codigo { get; private set; }
        public string Mensagem { get; private set; }
        public ENivelAlerta Nivel { get; private set; }

        public Alerta(string codigo, string mensagem, ENivelAlerta nivel)
        {
            Codigo = codigo;
            Mensagem = mensagem;
            Nivel = nivel;
        }
    }

    public enum ENivelAlerta
    {
        GRAVE,
        BAIXO,
        MEDIO
    }
}