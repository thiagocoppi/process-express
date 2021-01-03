using Domain.Ofx;
using Domain.Transacoes;
using Microsoft.AspNetCore.Hosting;
using OfxSharp;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace OFX.ImportacaoOfx
{
    public sealed class OfxReader : IOfxReader
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public OfxReader(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public Task<List<Transacao>> RealizarImportacoes()
        {
            var arquivosImportar = Directory.GetFiles(Path.Combine(_hostingEnvironment.WebRootPath, "arquivos"));

            foreach (var file in arquivosImportar)
            {
                var parser = new OFXDocumentParser();
                var ofxDocument = parser.Import(new FileStream(file, FileMode.Open));

                foreach(var transaction in ofxDocument.Transactions)
                {

                }
            }


            return null;
        }
    }
}