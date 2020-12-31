using System;

namespace Domain.Exceptions
{
    [Serializable]
    public sealed class BusinessException : Exception
    {
        public string Mensagem { get; private set; }

        public BusinessException(string mensagem) : base(mensagem)
        {
            Mensagem = mensagem;
        }
    }
}