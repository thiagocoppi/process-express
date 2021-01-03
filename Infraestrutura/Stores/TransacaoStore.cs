using Dapper;
using Domain.Transacoes;
using Infraestrutura.Context;
using System.Threading.Tasks;

namespace Infraestrutura.Stores
{
    public sealed class TransacaoStore : ITransacaoStore
    {
        private readonly IProcessExpressContext _processExpressContext;

        public TransacaoStore(IProcessExpressContext processExpressContext)
        {
            _processExpressContext = processExpressContext;
        }

        public async Task SalvarTransacao(Transacao transacao)
        {
            await _processExpressContext.GetConnection().ExecuteAsync(SQL_INSERIR_TRANSACAO, new
            {
                conta_id = transacao.Conta.Id,
                tipo_transacao = transacao.TipoTransacao.ToString(),
                data_transacao = transacao.DataTransacao.Date,
                valor = transacao.Valor,
                protocolo = transacao.Protocolo,
                codigo_referencia = transacao.NumeroReferencia,
                identificador_transacao = transacao.IdentificadorTransacao,
                descricao = transacao.Descricao
            });
        }

        private const string SQL_INSERIR_TRANSACAO =
            @"INSERT INTO TRANSACAO (CONTA_ID, TIPO_TRANSACAO, DATA_TRANSACAO, VALOR, PROTOCOLO, CODIGO_REFERENCIA, IDENTIFICADOR_TRANSACAO, DESCRICAO)
                SELECT :conta_id, :tipo_transacao, :data_transacao, :valor, :protocolo, :codigo_referencia, :identificador_transacao, :descricao WHERE
                    NOT EXISTS (SELECT id FROM TRANSACAO WHERE CONTA_ID = :conta_id AND IDENTIFICADOR_TRANSACAO = :identificador_transacao)";
    }
}