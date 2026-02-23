using Xunit;
using Ecommerce.API.Services;
using Ecommerce.API.Models;
using Ecommerce.API.Enums;

namespace Ecommerce.Tests;

public class PedidoServiceTests
{
    [Fact]
    public void CriarPedido_DeveAtribuirStatusCriado_ERegistrarHistorico()
    {
        // Arrange
        var historicoService = new HistoricoService();
        var pedidoService = new PedidoService(historicoService);
        var pedido = new Pedido
        {
            ClienteId = 1,
            Itens = new List<ItemPedido> { new ItemPedido { ProdutoId = 1, Quantidade = 2, PrecoUnitario = 50.0m } }
        };

        // Act
        var novoPedido = pedidoService.AdicionarPedidoAsync(pedido).Result;

        // Assert
        Assert.Equal(StatusPedido.Criado, novoPedido.Status);
        Assert.True(novoPedido.Id > 0);
        Assert.Equal(100.0m, novoPedido.ValorTotal);
        
        var historicos = historicoService.ObterHistoricosPorEntidadeAsync(nameof(Pedido), novoPedido.Id).Result.ToList();
        Assert.Single(historicos);
        Assert.Equal(AcaoHistorico.Criacao, historicos.First().Acao);
    }

    [Fact]
    public void PagarPedido_DeveMudarStatusParaPago()
    {
        // Arrange
        var historicoService = new HistoricoService();
        var pedidoService = new PedidoService(historicoService);
        var pedido = pedidoService.AdicionarPedidoAsync(new Pedido { ClienteId = 1 }).Result;

        // Act
        var sucesso = pedidoService.PagarPedidoAsync(pedido.Id).Result;

        // Assert
        Assert.True(sucesso);
        Assert.Equal(StatusPedido.Pago, pedidoService.ObterPedidoPorIdAsync(pedido.Id).Result!.Status);
        
        var historicos = historicoService.ObterHistoricosPorEntidadeAsync(nameof(Pedido), pedido.Id).Result.ToList();
        Assert.Contains(historicos, h => h.Acao == AcaoHistorico.Alteracao && h.Detalhes.Contains("pago"));
    }

    [Fact]
    public void CancelarPedido_NaoPodeCancelarSeEstiverPago()
    {
        // Arrange
        var historicoService = new HistoricoService();
        var pedidoService = new PedidoService(historicoService);
        var pedido = pedidoService.AdicionarPedidoAsync(new Pedido { ClienteId = 1 }).Result;
        pedidoService.PagarPedidoAsync(pedido.Id).Wait();

        // Act
        var sucesso = pedidoService.CancelarPedidoAsync(pedido.Id).Result;

        // Assert
        Assert.False(sucesso);
        Assert.Equal(StatusPedido.Pago, pedidoService.ObterPedidoPorIdAsync(pedido.Id).Result!.Status);
    }

    [Fact]
    public void CancelarPedido_DeveMudarStatusParaCanceladoSeNaoEstiverPago()
    {
        // Arrange
        var historicoService = new HistoricoService();
        var pedidoService = new PedidoService(historicoService);
        var pedido = pedidoService.AdicionarPedidoAsync(new Pedido { ClienteId = 1 }).Result;

        // Act
        var sucesso = pedidoService.CancelarPedidoAsync(pedido.Id).Result;

        // Assert
        Assert.True(sucesso);
        Assert.Equal(StatusPedido.Cancelado, pedidoService.ObterPedidoPorIdAsync(pedido.Id).Result!.Status);
    }
}
