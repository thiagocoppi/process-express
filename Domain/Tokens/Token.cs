namespace Domain.Tokens
{
    public sealed class Token
    {
        public string Info { get; private set; }

        public Token(string info)
        {
            Info = info;
        }
    }
}