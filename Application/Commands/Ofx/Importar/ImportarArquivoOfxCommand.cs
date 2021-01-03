using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Commands.Ofx.Importar
{
    public sealed class ImportarArquivoOfxCommand : IRequest<ImportarArquivoOfxCommandResult>
    {
        public IFormFile Arquivo { get; set; }
    }
}