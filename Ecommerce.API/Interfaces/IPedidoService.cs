using Ecommerce.API.Models;
using Ecommerce.API.Enums;

namespace Ecommerce.API.Interfaces
{
    public interface IPedidoService
    {
        Task<IEnumerable<Pedido>> ObterTodosPedidosAsync();
        Task<IEnumerable<Pedido>> ObterPedidosPorStatusAsync(StatusPedido status);
        Task<Pedido?> ObterPedidoPorIdAsync(int id);
        Task<Pedido> AdicionarPedidoAsync(Pedido pedido);
        Task<bool> PagarPedidoAsync(int id);
        Task<bool> CancelarPedidoAsync(int id);
    }
}