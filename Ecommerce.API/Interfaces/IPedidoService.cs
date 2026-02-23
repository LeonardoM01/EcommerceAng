using Ecommerce.API.Models;

namespace Ecommerce.API.Interfaces
{
    public interface IPedidoService
    {
        Task<IEnumerable<Pedido>> ObterTodosPedidosAsync();
        Task<Pedido>? ObterPedidoPorStatus(Enums.StatusPedido status);
        Task<Pedido>? ObterPedidoPorIdAsync(int id);
        Task<Pedido> AdicionarPedidoAsync(Pedido pedido);
        Task<bool> PagarPedidoAsync(int id);
        Task<bool> CancelarPedidoAsync(int id);
    }
}