using Application.Commands.ContaCorrente.AtualizarDadosCadastrais;
using Application.Commands.ContaCorrente.Lancamento;
using Application.Queries.ContaCorrente.BuscarConta;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ProcessExpress.Controllers
{
    [Route("process-express/api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class ContaBancariaController : ApiController
    {
        [HttpGet("buscar-conta/{id}")]
        public async Task<ActionResult<BuscarContaQueryResult>> BuscarConta([FromRoute] Guid id)
        {   
            return await Mediator.Send(new BuscarContaQueryRequest() { Id = id });
        }

        [HttpPost("lancamento-financeiro-manual")]
        public async Task<ActionResult<RealizarLancamentoFinanceiroCommandResult>> RealizaLancamentoFinanceiro([FromBody] RealizarLancamentoFinanceiroCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPatch("atualizar-dados")]
        public async Task<ActionResult<Unit>> AtualizarDados([FromBody] AtualizarDadosCadastraisCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}