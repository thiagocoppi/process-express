using Dapper;
using Domain.Transacoes;
using Infraestrutura.Context;
using System;
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

        public async Task<Transacao> SalvarTransacaoComIdentificadorUnico(Transacao transacao)
        {
            var guidTransacao = await _processExpressContext.GetConnection().ExecuteScalarAsync<Guid>(SQL_INSERIR_TRANSACAO_COM_IDENTIFICADOR_UNICO, new
            {
                conta_id = transacao.Conta.Id,
                tipo_transacao = transacao.TipoTransacao.ToString(),
                data_transacao = transacao.DataTransacao.Date,
                valor = transacao.Valor,
                protocolo = transacao.Protocolo,
                codigo_referencia = transacao.NumeroReferencia,
                identificador_transacao = transacao.IdentificadorTransacao,
                descricao = transacao.Descricao,
                hash = transacao.Hash                
            });

            transacao.AlterarIdentificador(guidTransacao);

            return transacao;
        }

        public async Task<Transacao> SalvarTransacao(Transacao transacao)
        {
            var guidTransacao = await _processExpressContext.GetConnection().ExecuteScalarAsync<Guid>(SQL_INSERIR_TRANSACAO, new
            {
                conta_id = transacao.Conta.Id,
                tipo_transacao = transacao.TipoTransacao.ToString(),
                data_transacao = transacao.DataTransacao.Date,
                valor = transacao.Valor,
                protocolo = transacao.Protocolo,
                codigo_referencia = transacao.NumeroReferencia,
                identificador_transacao = transacao.IdentificadorTransacao,
                descricao = transacao.Descricao,
                hash = transacao.Hash
            });

            transacao.AlterarIdentificador(guidTransacao);

            return transacao;
        }

        public async Task<bool> VerificarHashExiste(string hash)
        {
            return await _processExpressContext.GetConnection().QueryFirstOrDefaultAsync<bool>(SQL_VERIFICAR_TRANSACA, new
            {
                hashTransacao = hash
            });
        }

        public async Task<bool> VerificarIdentificadorUnicoTransacaoFiscoExiste(long identificador)
        {
            return await _processExpressContext.GetConnection().QueryFirstOrDefaultAsync<bool>(SQL_VERIFICAR_TRANSACAO_PELO_ID_UNICO, new
            {
                identificadorTransacao = identificador
            });
        }

        private const string SQL_INSERIR_TRANSACAO_COM_IDENTIFICADOR_UNICO =
            @"INSERT INTO TRANSACAO (CONTA_ID, TIPO_TRANSACAO, DATA_TRANSACAO, VALOR, PROTOCOLO, CODIGO_REFERENCIA, IDENTIFICADOR_TRANSACAO, DESCRICAO, HASH)
                SELECT :conta_id, :tipo_transacao, :data_transacao, :valor, :protocolo, :codigo_referencia, :identificador_transacao, :descricao, :hash WHERE
                    NOT EXISTS (SELECT id FROM TRANSACAO WHERE CONTA_ID = :conta_id AND IDENTIFICADOR_TRANSACAO = :identificador_transacao)
                RETURNING id";

        private const string SQL_INSERIR_TRANSACAO =
            @"INSERT INTO TRANSACAO (CONTA_ID, TIPO_TRANSACAO, DATA_TRANSACAO, VALOR, PROTOCOLO, CODIGO_REFERENCIA, IDENTIFICADOR_TRANSACAO, DESCRICAO, HASH)
                VALUES (:conta_id, :tipo_transacao, :data_transacao, :valor, :protocolo, :codigo_referencia, :identificador_transacao, :descricao, :hash)";

        private const string SQL_VERIFICAR_TRANSACA =
            @"SELECT 1 FROM TRANSACAO WHERE HASH = :hashTransacao";

        private const string SQL_VERIFICAR_TRANSACAO_PELO_ID_UNICO =
            @"SELECT 1 FROM TRANSACAO WHERE identificador_transacao = :identificadorTransacao";
    }
}