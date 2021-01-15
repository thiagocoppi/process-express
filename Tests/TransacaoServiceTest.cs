using Domain.Exceptions;
using Domain.Transacoes;
using FluentAssertions;
using Languages;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Tests.Base;

namespace Tests
{
    public sealed class TransacaoServiceTest : BaseTestIntegration
    {
        [Test]
        public void Dado_LancamentoNulo_Entao_Deve_Retornar_Critica()
        {
            using var scope = StartupTest().CreateScope();
            var transacaoService = scope.ServiceProvider.GetService<ITransacaoService>();
            var exception = Assert.ThrowsAsync<BusinessException>(() => transacaoService.RealizarLancamento(null));
            exception.Mensagem.Should().Be(Messages.TransacaoInvalida);
        }

        //[Test]
        //public void Dado_ContaInexistente_Entao_Deve_Retornar_Critica()
        //{
        //    using var scope = StartupTest().CreateScope();
        //    var transacaoService = scope.ServiceProvider.GetService<ITransacaoService>();
        //    var mockRetornoContaStore = Substitute.For<IContaStore>();
        //    mockRetornoContaStore.BuscarConta(Arg.Any<Conta>()).Returns(Task.FromResult((Conta)null));
        //    var exception = Assert.ThrowsAsync<BusinessException>(() => transacaoService.RealizarLancamento(new Transacao(new Conta(new Banco("BB", 1), 1, 1), 
        //        ETipoTransacao.OTHER, DateTime.Now, 10, "ABC", "ABC", "Teste")));
        //    exception.Mensagem.Should().Be(string.Format(Messages.ContaInexistente, 1, 1));
        //}
    }
}