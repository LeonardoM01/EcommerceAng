using Ecommerce.API.Models;

namespace Ecommerce.API.Interfaces
{
    public interface IClienteService
    {
        Task<IEnumerable<Cliente>> ObterTodosClientesAsync();
        Task<Cliente>? ObterClientePorIdAsync(int id);
        Task<Cliente> AdicionarClienteAsync(Cliente cliente);
        Task<bool> AtualizarClienteAsync(int id, Cliente cliente);        
    }
}