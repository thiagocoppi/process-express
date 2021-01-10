using Infraestrutura.Models.ContaCorrente;
using System.Collections.Generic;

namespace Infraestrutura.Models.Contabilidade.RelatorioTransacoes
{
    public class RelatorioTransacoesDto
    {
        public ContaCorrenteDto ContaCorrente { get; set; }
        public IList<TransacaoDto> TransacoesOcorridas { get; set; }
    }
}