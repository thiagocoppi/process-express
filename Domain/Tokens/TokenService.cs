using Domain.Exceptions;
using Languages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Tokens
{
    public sealed class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<TokenService> _logger;

        public TokenService(IConfiguration configuration, ILogger<TokenService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public Task<Identificacao> AbrirToken(Token token)
        {
            _logger.LogInformation("Abrindo o token {0}", token.Info);
            var handler = new JwtSecurityTokenHandler();
            var tokenRead = handler.ReadToken(token.Info) as JwtSecurityToken;

            if (tokenRead is null)
            {
                _logger.LogError("Ocorreu um erro ao abrir o token {0}", token.Info);
                throw new BusinessException(Messages.TokenInvalido);
            }

            var nomeInsitituicao = tokenRead.Claims.First(r => r.Type == "Instituicao");
            var funcionario = tokenRead.Claims.First(r => r.Type == "Funcionario");
            var data = tokenRead.Claims.First(r => r.Type == "Data");

            return Task.FromResult(new Identificacao(nomeInsitituicao.Value, DateTime.Parse(data.Value), funcionario.Value));
        }

        public Task<Token> GerarToken(Identificacao identificacao)
        {
            _logger.LogInformation("Gerando um token para a instituição {0} em nome do funcionário {1}", identificacao.NomeInstituicao, 
                identificacao.NomeFuncionario);

            var applicationSecret = _configuration.GetValue<string>("ApplicationSecret");
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(applicationSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Instituicao", identificacao.NomeInstituicao),
                    new Claim("Funcionario", identificacao.NomeFuncionario),
                    new Claim("Data", identificacao.Data.ToShortDateString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Task.FromResult(new Token(tokenHandler.WriteToken(token)));
        }
    }
}