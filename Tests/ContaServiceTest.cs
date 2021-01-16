using Domain.ContaCorrentes;
using Domain.Exceptions;
using Domain.Transacoes;
using FluentAssertions;
using Languages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute;
using NUnit.Framework;
using Tests.Base;

namespace Tests
{
    public sealed class ContaServiceTest : BaseTestIntegration
    {
        [Test]
        public void Dado_ContaNula_Entao_Deve_Retornar_Critica()
        {
            using var scope = StartupTest().CreateScope();
            var mockRetornoContaStore = Substitute.For<IContaStore>();
            var contaService = new ContaService(mockRetornoContaStore, new NullLogger<ContaService>());
            var exception = Assert.ThrowsAsync<BusinessException>(() => contaService.AtualizarDadosCadastrais(null));
            exception.Mensagem.Should().Be(Messages.ContaInvalidaParaAtualizarDados);
        }
    }
}