using Ecommerce.API.Models;

namespace Ecommerce.API.Interfaces
{
    public interface IProdutoService
    {
        Task<IEnumerable<Produto>> ObterTodosProdutosAsync();
        Task<Produto>? ObterProdutoPorIdAsync(int id);
        Task<Produto> AdicionarProdutoAsync(Produto produto);
        Task<bool> AtualizarProdutoAsync(int id, Produto produto);
    }
}