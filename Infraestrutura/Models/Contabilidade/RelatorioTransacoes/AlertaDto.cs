namespace Infraestrutura.Models.Contabilidade.RelatorioTransacoes
{
    public class AlertaDto
    {
        public string Codigo { get; set; }
        public string Mensagem { get; set; }
        public ENivelAlerta Nivel { get; set; }
    }
}
