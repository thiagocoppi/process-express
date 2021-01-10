using Application.Commands.Login;
using Application.Commands.Tokens.AbrirToken;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ProcessExpress.Controllers
{
    [Route("process-express/api/v{version:apiVersion}/[controller]")]
    public class AuthorizeController : ApiController
    {
        /// <summary>
        /// Realiza o login na aplicação ProcessExpress com as informações para processar o arquivo OFX
        /// </summary>
        /// <param name="command">Comando para realizar o login</param>
        /// <returns>Token para acesso à aplicação</returns>
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<CreateLoginCommandResult>> Authenticate([FromBody] CreateLoginCommand command)
        {
            return await Mediator.Send(command);
        }

        /// <summary>
        /// Realiza a abertura de um token para verificar o conteúdo que há dentro
        /// </summary>
        /// <param name="command">Commando contendo o token</param>
        /// <returns>As informações contida dentro do token</returns>
        [HttpPost]
        [Route("abrir")]
        public async Task<ActionResult<AbrirTokenCommandResult>> AbrirToken([FromBody] AbrirTokenCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}