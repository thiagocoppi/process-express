using System.Text.RegularExpressions;

namespace Domain.Transacoes
{
    public static class TransacaoExtension
    {
        public static int ApenasNumeros(this string numeroConta)
        {
            numeroConta = Regex.Replace(numeroConta, "[^0-9]", "");

            return int.Parse(numeroConta);
        }
    }
}