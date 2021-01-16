using Domain.Exceptions;
using Domain.Transacoes;
using FluentAssertions;
using Languages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
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
            var mockRetornoContaStore = Substitute.For<IContaStore>();
            var mockRetornoTransacaoStore = Substitute.For<ITransacaoStore>();
            ITransacaoService transacaoService = new TransacaoService(mockRetornoTransacaoStore, new NullLogger<TransacaoService>(), mockRetornoContaStore);
            var exception = Assert.ThrowsAsync<BusinessException>(() => transacaoService.RealizarLancamento(null));
            exception.Mensagem.Should().Be(Messages.TransacaoInvalida);
        }

        [Test]
        public void Dado_ContaInexistente_Entao_Deve_Retornar_Critica()
        {
            using var scope = StartupTest().CreateScope();
            var mockRetornoContaStore = Substitute.For<IContaStore>();
            var mockRetornoTransacaoStore = Substitute.For<ITransacaoStore>();            
            mockRetornoContaStore.BuscarConta(Arg.Any<Conta>()).Returns(Task.FromResult((Conta)null));
            ITransacaoService transacaoService = new TransacaoService(mockRetornoTransacaoStore, new NullLogger<TransacaoService>(), mockRetornoContaStore);
            var exception = Assert.ThrowsAsync<BusinessException>(() => transacaoService.RealizarLancamento(new Transacao(new Conta(new Banco("BB", 1), 1, 1),
                ETipoTransacao.OTHER, DateTime.Now, 10, "ABC", "ABC", "Teste")));
            exception.Mensagem.Should().Be(string.Format(Messages.ContaInexistente, 1, 1));
        }

        [Test]
        public void Dado_TransacaoJaCadastada_PeloIdentificadorUnico_Entao_DeveRetornar_Critica()
        {
            using var scope = StartupTest().CreateScope();
            var mockRetornoContaStore = Substitute.For<IContaStore>();
            var mockRetornoTransacaoStore = Substitute.For<ITransacaoStore>();
            mockRetornoContaStore.BuscarConta(Arg.Any<Conta>()).Returns(Task.FromResult(new Conta(Guid.NewGuid(), DateTime.Now, "Neymar da Silva", "33235895")));
            mockRetornoTransacaoStore.VerificarHashExiste(Arg.Any<string>()).Returns(true);
            mockRetornoTransacaoStore.VerificarIdentificadorUnicoTransacaoFiscoExiste(Arg.Any<long>()).Returns(true);
            
            ITransacaoService transacaoService = new TransacaoService(mockRetornoTransacaoStore, new NullLogger<TransacaoService>(), mockRetornoContaStore);
            var exception = Assert.ThrowsAsync<BusinessException>(() => transacaoService.RealizarLancamento(new Transacao(new Conta(new Banco("BB", 1), 1, 1),
                ETipoTransacao.OTHER, DateTime.Now, 10, "ABC", "ABC", "Teste", 100)));

            exception.Mensagem.Should().Be(string.Format(Messages.LancamentoDuplicado, 100));
        }

        [Test]
        public void Dado_TransacaoHashJaCadstrado_EmiteAlerta_Deve_RetornarTransacaoCadastrada()
        {
            var transacaoInserir = new Transacao(new Conta(new Banco("BB", 1), 1, 1),
                ETipoTransacao.OTHER, DateTime.Now, 10, "ABC", "ABC", "Teste", 100);

            transacaoInserir.AlterarIdentificador(Guid.NewGuid());

            using var scope = StartupTest().CreateScope();
            var mockRetornoContaStore = Substitute.For<IContaStore>();
            var mockRetornoTransacaoStore = Substitute.For<ITransacaoStore>();
            mockRetornoContaStore.BuscarConta(Arg.Any<Conta>()).Returns(Task.FromResult(new Conta(Guid.NewGuid(), DateTime.Now, "Neymar da Silva", "33235895")));
            mockRetornoTransacaoStore.VerificarHashExiste(Arg.Any<string>()).Returns(true);
            mockRetornoTransacaoStore.VerificarIdentificadorUnicoTransacaoFiscoExiste(Arg.Any<long>()).Returns(false);
            mockRetornoTransacaoStore.SalvarTransacaoComIdentificadorUnico(Arg.Any<Transacao>()).Returns(transacaoInserir);

            ITransacaoService transacaoService = new TransacaoService(mockRetornoTransacaoStore, new NullLogger<TransacaoService>(), mockRetornoContaStore);
            var transacaoInserida = transacaoService.RealizarLancamento(transacaoInserir).GetAwaiter().GetResult();
            transacaoInserida.Alerta.Should().NotBeNullOrEmpty();
        }
    }
}