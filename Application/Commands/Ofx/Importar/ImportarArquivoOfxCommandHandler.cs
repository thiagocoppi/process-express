using Domain.Ofx;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Commands.Ofx.Importar
{
    public sealed class ImportarArquivoOfxCommandHandler : IRequestHandler<ImportarArquivoOfxCommand, ImportarArquivoOfxCommandResult>
    {
        private readonly IOfxReader _ofxReader;

        public ImportarArquivoOfxCommandHandler(IOfxReader ofxReader)
        {
            _ofxReader = ofxReader;
        }

        public async Task<ImportarArquivoOfxCommandResult> Handle(ImportarArquivoOfxCommand request, CancellationToken cancellationToken)
        {
            await _ofxReader.RealizarImportacoes();

            return new ImportarArquivoOfxCommandResult();
        }
    }
}