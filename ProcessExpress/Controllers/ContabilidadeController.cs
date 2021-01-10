using Application.Queries.Contabilidade.RelatorioTransacoes;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ProcessExpress.Controllers
{
    [Route("process-express/api/v{version:apiVersion}/[controller]")]
    //[Authorize]
    public class ContabilidadeController : ApiController
    {

        [HttpPost("gerar-relatorio")]
        public async Task<ActionResult<GerarRelatorioTransacoesContasQueryResult>> RealizaLancamentoFinanceiro([FromBody] GerarRelatorioTransacoesContasQueryRequest command)
        {
            var retornoPaginadoRelatorio = await Mediator.Send(command);

            var paginacaoResult = new
            {
                retornoPaginadoRelatorio.TransacoesClientes.PageSize,
                retornoPaginadoRelatorio.TransacoesClientes.TotalCount,
                retornoPaginadoRelatorio.TransacoesClientes.TotalPages,
                retornoPaginadoRelatorio.TransacoesClientes.HasNext,
                retornoPaginadoRelatorio.TransacoesClientes.HasPrevious
            };

            Response.Headers.Add("X-Paginacao", JsonConvert.SerializeObject(paginacaoResult));

            return retornoPaginadoRelatorio;
        }
    }
}
