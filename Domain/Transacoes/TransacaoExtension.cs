using System.Text.RegularExpressions;

namespace Domain.Transacoes
{
    public static class TransacaoExtension
    {
        public static long ApenasNumeros(this string normalize)
        {
            normalize = Regex.Replace(normalize, "[^0-9]", "");

            return long.Parse(normalize);
        }
    }
}