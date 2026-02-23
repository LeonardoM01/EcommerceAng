using Ecommerce.API.Models;
using Ecommerce.API.Interfaces;
using Ecommerce.API.Enums;
using System.Runtime.CompilerServices;

namespace Ecommerce.API.Services;

public class PedidoService : IPedidoService
{
    private readonly List<Pedido> _pedidos = new();
    private int _nextId = 1;
    private readonly IHistoricoService _historicoService;

    public PedidoService(IHistoricoService historicoService)
    {
        _historicoService = historicoService;
    }

    
    public Task<IEnumerable<Pedido>> ObterTodosPedidosAsync()
    {
        return Task.FromResult<IEnumerable<Pedido>>(_pedidos);
    }

    public Task<Pedido?> ObterPedidoPorIdAsync(int id)
    {
        var pedido = _pedidos.FirstOrDefault(p => p.Id == id);
        return Task.FromResult(pedido);
    }

    public Task<IEnumerable<Pedido>> ObterPedidosPorStatusAsync(StatusPedido status)    
    {
        var pedidos = _pedidos.Where(p => p.Status == status);
        return Task.FromResult(pedidos);
    }

    public async Task<Pedido> AdicionarPedidoAsync(Pedido pedido)
    {
        pedido.Id = _nextId++;
        pedido.DataPedido = pedido.DataPedido == default
            ? DateTime.UtcNow
            : pedido.DataPedido;

        pedido.Status = StatusPedido.Criado;
        _pedidos.Add(pedido);

        await _historicoService.RegistrarHistorico(
            nameof(Pedido),
            pedido.Id,
            AcaoHistorico.Criacao,
            $"Pedido criado com status {pedido.Status}."
        );

        return pedido;
    }

    public async Task<bool> PagarPedidoAsync(int id)
    {
        var pedido = _pedidos.FirstOrDefault(p => p.Id == id);
        if (pedido is null || pedido.Status != StatusPedido.Criado)
            return false;

        pedido.Status = StatusPedido.Pago;

        await _historicoService.RegistrarHistorico(
            nameof(Pedido),
            pedido.Id,
            AcaoHistorico.Alteracao,
            $"Pedido pago. Status atualizado para {pedido.Status}."
        );

        return true;
    }

    public async Task<bool> CancelarPedidoAsync(int id)
    {
        var pedido = _pedidos.FirstOrDefault(p => p.Id == id);

        if (pedido is null)
            return false;

        if (pedido.Status is StatusPedido.Pago or StatusPedido.Cancelado)
            return false;

        pedido.Status = StatusPedido.Cancelado;

        await _historicoService.RegistrarHistorico(
            nameof(Pedido),
            pedido.Id,
            AcaoHistorico.Alteracao,
            $"Pedido cancelado. Status atualizado para {pedido.Status}."
        );

        return true;
    }
}