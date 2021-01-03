using Application.Commands.Ofx.Importar;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ProcessExpress.Controllers
{
    public class OfxController : ApiController
    {
        [HttpPost]
        [Route("processar-ofx")]
        //[Authorize]
        public async Task<ActionResult<ImportarArquivoOfxCommandResult>> ProcessarOfx()
        {
            return await Mediator.Send(new ImportarArquivoOfxCommand());
        }
    }
}