using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.Ofx.Importar
{
    public sealed class ImportarArquivoOfxCommandHandler : IRequestHandler<ImportarArquivoOfxCommand, ImportarArquivoOfxCommandResult>
    {
        public Task<ImportarArquivoOfxCommandResult> Handle(ImportarArquivoOfxCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}