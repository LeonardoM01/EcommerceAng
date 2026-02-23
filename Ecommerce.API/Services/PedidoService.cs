using Ecommerce.API.Models;
using Ecommerce.API.Interfaces;
using Ecommerce.API.Enums;
using System.Runtime.CompilerServices;

namespace Ecommerce.API.Services;

public class PedidoService : IPedidoService
{
   private readonly List<Pedido> _pedidos = new List<Pedido>();
   private int _nextId = 1;
   private readonly IHistoricoService _historicoService;

    public PedidoService(IHistoricoService historicoService)
    {
         _historicoService = historicoService;
    }

    public async Task<IEnumerable<Pedido>> ObterTodosPedidosAsync()
    {
        return await Task.FromResult(_pedidos);
    }

    public async Task<Pedido>? ObterPedidosPorStatusAsync(StatusPedido status)
    {
        var pedido = _pedidos.FirstOrDefault(p => p.Status == status);
        return await Task.FromResult(pedido);
    }

    public async Task<Pedido>? ObterPedidoPorIdAsync(int id)
    {
        var pedido = _pedidos.FirstOrDefault(p => p.Id == id);
        return await Task.FromResult(pedido);
    }

    public async Task<Pedido> AdicionarPedidoAsync(Pedido pedido)
    {
        pedido.Id = _nextId++;
        if (pedido.DataPedido == default)
            pedido.DataPedido = DateTime.UtcNow;
        pedido.Status = StatusPedido.Criado;
        _pedidos.Add(pedido);

        await _historicoService.RegistrarHistorico(nameof(Pedido), pedido.Id, AcaoHistorico.Criacao, $"Pedido criado com status {pedido.Status}.");

        return pedido;
    }

    public async Task<bool> PagarPedidoAsync(int id)
    {
        var pedido = _pedidos.FirstOrDefault(p => p.Id == id);
        if (pedido == null || pedido.Status != StatusPedido.Criado) return false;

        pedido.Status = StatusPedido.Pago;

        await _historicoService.RegistrarHistorico(nameof(Pedido), pedido.Id, AcaoHistorico.Alteracao, $"Pedido pago. Status atualizado para {pedido.Status}.");

        return true;
    }

    public async Task<bool> CancelarPedidoAsync(int id)
    {
        var pedido = _pedidos.FirstOrDefault(p => p.Id == id);
        if (pedido == null || pedido.Status == StatusPedido.Cancelado) return false;

        // não permitir cancelamento de pedidos já pagos
        if (pedido.Status == StatusPedido.Pago) return false;

        pedido.Status = StatusPedido.Cancelado;

        await _historicoService.RegistrarHistorico(nameof(Pedido), pedido.Id, AcaoHistorico.Alteracao, $"Pedido cancelado. Status atualizado para {pedido.Status}.");

        return true;
    }
}