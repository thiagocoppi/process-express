using Domain.ContaCorrentes;
using Domain.Exceptions;
using FluentAssertions;
using Languages;
using Microsoft.Extensions.DependencyInjection;
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
            var contaService = scope.ServiceProvider.GetService<IContaService>();
            var exception = Assert.ThrowsAsync<BusinessException>(() => contaService.AtualizarDadosCadastrais(null));
            exception.Mensagem.Should().Be(Messages.ContaInvalidaParaAtualizarDados);
        }
    }
}