using Domain.Exceptions;
using Languages;

namespace Domain.Tokens
{
    public sealed class Token
    {
        public string Info { get; private set; }

        public Token(string info)
        {
            if (string.IsNullOrEmpty(info))
            {
                throw new BusinessException(Messages.TokenInvalidoParaCriacao);
            }

            Info = info;
        }
    }
}