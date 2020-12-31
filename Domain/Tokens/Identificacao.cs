using System;

namespace Domain.Tokens
{
    public class Identificacao
    {
        public string NomeInstituicao { get; private set; }
        public DateTime Data { get; private set; }
        public string NomeFuncionario { get; private set; }

        public Identificacao(string nomeInstituicao, DateTime data, string nomeFuncionario)
        {
            NomeInstituicao = nomeInstituicao;
            Data = data;
            NomeFuncionario = nomeFuncionario;
        }
    }
}