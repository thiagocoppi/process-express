using System;

namespace Infraestrutura.Models.Contabilidade.RelatorioTransacoes
{
    public class TransacaoDto
    {
        public string TipoTransacao { get; set; }
        public DateTime DataTransacao { get; set; }
        public decimal Valor { get; set; }
        public string Protocolo { get; set; }
        public string NumeroReferencia { get; set; }
        public string Descricao { get; set; }
        public long IdentificadorTransacao { get; set; }
        public string Hash { get; set; }
        public AlertaDto Alerta { get; set; }
        public TransacaoDto TransacaoHashDuplicado { get; set; }
    }
}
