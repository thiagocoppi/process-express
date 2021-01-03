using Domain.Ofx;
using Domain.Transacoes;
using Microsoft.AspNetCore.Hosting;
using OfxSharp;
using System;
using System.IO;
using System.Threading.Tasks;

namespace OFX.ImportacaoOfx
{
    public sealed class OfxReader : IOfxReader
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IBancoStore _bancoStore;
        private readonly IContaStore _contaStore;
        private readonly ITransacaoStore _transacaoStore;

        public OfxReader(IWebHostEnvironment hostingEnvironment, IBancoStore bancoStore, IContaStore contaStore, ITransacaoStore transacaoStore)
        {
            _hostingEnvironment = hostingEnvironment;
            _bancoStore = bancoStore;
            _contaStore = contaStore;
            _transacaoStore = transacaoStore;
        }

        public async Task RealizarImportacoes()
        {
            var arquivosImportar = Directory.GetFiles(Path.Combine(_hostingEnvironment.WebRootPath, "arquivos"));

            foreach (var file in arquivosImportar)
            {
                var parser = new OFXDocumentParser();
                var ofxDocument = parser.Import(new FileStream(file, FileMode.Open));
                var banco = new Banco(string.Empty, int.Parse(ofxDocument.Account.BankId));
                var conta = new Conta(banco, ofxDocument.Account.AccountId.ApenasNumeros(), ofxDocument.Account.BranchId.ApenasNumeros());

                foreach (var transaction in ofxDocument.Transactions)
                {
                    var transacao = new Transacao(conta, (ETipoTransacao)Enum.Parse(typeof(ETipoTransacao), transaction.TransType.ToString()), transaction.Date, transaction.Amount,
                        ulong.Parse(transaction.CheckNum), transaction.ReferenceNumber.ApenasNumeros(), transaction.Memo, ulong.Parse(transaction.TransactionId));

                    banco = await _bancoStore.BuscarBancoPeloCodigo(banco.Codigo);

                    if (banco is null)
                    {
                        await _bancoStore.RegistrarBanco(banco);
                    }

                    conta.AlterarBanco(banco);

                    await _contaStore.RegistrarConta(conta);


                }
            }
        }
    }
}