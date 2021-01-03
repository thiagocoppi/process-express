using Domain.Exceptions;
using Domain.Ofx;
using Domain.Transacoes;
using Languages;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<OfxReader> _logger;

        public OfxReader(IWebHostEnvironment hostingEnvironment, IBancoStore bancoStore, IContaStore contaStore, ITransacaoStore transacaoStore, ILogger<OfxReader> logger)
        {
            _hostingEnvironment = hostingEnvironment;
            _bancoStore = bancoStore;
            _contaStore = contaStore;
            _transacaoStore = transacaoStore;
            _logger = logger;
        }

        public async Task RealizarImportacoes()
        {
            _logger.LogInformation("Iniciando processo de importação dos arquivos OFX");
            var arquivosImportar = Directory.GetFiles(Path.Combine(_hostingEnvironment.WebRootPath, "arquivos"));
            
            foreach (var file in arquivosImportar)
            {
                if (!file.ToLower().Contains(".ofx"))
                {
                    throw new BusinessException(string.Format(Messages.TipoArquivoInvalido, file));
                }

                var parser = new OFXDocumentParser();
                var ofxDocument = parser.Import(new FileStream(file, FileMode.Open));
                var banco = new Banco(string.Empty, int.Parse(ofxDocument.Account.BankId));
                var conta = new Conta(banco, ofxDocument.Account.AccountId.ApenasNumeros(), ofxDocument.Account.BranchId.ApenasNumeros());
                _logger.LogInformation("Iniciando importação das transações para a conta Numero/Agencia {0}/{1}", conta.Numero, conta.Agencia);
                foreach (var transaction in ofxDocument.Transactions)
                {
                    var bancosExistente = await _bancoStore.BuscarBancoPeloCodigo(banco.Codigo);

                    if (bancosExistente is null)
                    {
                        banco = await _bancoStore.RegistrarBanco(banco);
                    }

                    conta.AlterarBanco(bancosExistente ?? banco);

                    conta = await _contaStore.RegistrarConta(conta);

                    if (conta.Id == Guid.Empty)
                    {
                        var guidContaCadastrada = await _contaStore.BuscarIdentificadorConta(conta);
                        conta.AlterarIdentificador(guidContaCadastrada);
                    }

                    var transacao = new Transacao(conta, (ETipoTransacao)Enum.Parse(typeof(ETipoTransacao), transaction.TransType.ToString()), transaction.Date, transaction.Amount,
                        transaction.CheckNum, transaction.ReferenceNumber, transaction.Memo, long.Parse(transaction.TransactionId));

                    _logger.LogInformation("Inserindo a transação com a identificação {0}", transacao.IdentificadorTransacao);

                    await _transacaoStore.SalvarTransacao(transacao);
                }
            }
        }
    }
}