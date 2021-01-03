﻿using Dapper;
using Domain.Transacoes;
using Infraestrutura.Context;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Infraestrutura.Stores
{
    public sealed class BancoStore : IBancoStore
    {
        private readonly IProcessExpressContext _context;

        public BancoStore(IProcessExpressContext context)
        {
            _context = context;
        }

        public async Task<Banco> BuscarBancoPeloCodigo(int codigoBanco)
        {
            return await _context.GetConnection().QueryFirstOrDefaultAsync<Banco>(SQL_BUSCAR_BANCO_PELO_CODIGO, new
            {
                codigo = codigoBanco
            });
        }

        public async Task<Banco> RegistrarBanco(Banco banco)
        {
            var param = new DynamicParameters();
            param.Add("nome", banco.Nome, direction: ParameterDirection.Input);
            param.Add("codigo", banco.Codigo, direction: ParameterDirection.Input);
            param.Add("id", direction: ParameterDirection.Output);


            await _context.GetConnection().ExecuteAsync(SQL_REGISTRAR_BANCO, param);

            banco.AlterarIdentificador(param.Get<Guid>("id"));

            return banco;
        }

        private const string SQL_BUSCAR_BANCO_PELO_CODIGO =
            @"SELECT ID, NOME, CODIGO FROM BANCO WHERE CODIGO = :codigo";

        private const string SQL_REGISTRAR_BANCO =
            @"INSERT INTO BANCO(NOME,CODIGO) VALUES (:nome,:codigo) RETURNING :id";
    }
}