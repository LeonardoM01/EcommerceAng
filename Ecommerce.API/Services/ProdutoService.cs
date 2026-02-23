using Ecommerce.API.Models;
using Ecommerce.API.Interfaces;
using Ecommerce.API.Enums;
using System.Runtime.CompilerServices;

namespace Ecommerce.API.Services;

public class ProdutoService : IProdutoService
{
    private readonly List<Produto> _produtos = new List<Produto>();
    private int _nextId = 1;
    private readonly IHistoricoService _historicoService;

    public ProdutoService(IHistoricoService historicoService)
    {
        _historicoService = historicoService;
    }

    public async Task<IEnumerable<Produto>> ObterTodosProdutosAsync()
    {
        return await Task.FromResult(_produtos);
    }

    public async Task<Produto>? ObterProdutoPorIdAsync(int id)
    {
        var produto = _produtos.FirstOrDefault(p => p.Id == id);
        return await Task.FromResult(produto);
    }

    public async Task<Produto> AdicionarProdutoAsync(Produto produto)
    {
        produto.Id = _produtos.Count + 1;
        _produtos.Add(produto);

        await _historicoService.RegistrarHistorico(nameof(Produto), produto.Id, AcaoHistorico.Criacao, $"Produto {produto.Nome} criado com preço {produto.Preco}.");

        return produto;
    }

    public async Task<bool> AtualizarProdutoAsync(int id, Produto produto)
    {
        var produtoExistente = _produtos.FirstOrDefault(p => p.Id == id);
        if (produtoExistente == null) return false;

        var detalhes = $"Produto '{produtoExistente.Nome}' atualizado de preço {produtoExistente.Preco} para {produto.Preco}";

        produtoExistente.Nome = produto.Nome;
        produtoExistente.Preco = produto.Preco;

        await _historicoService.RegistrarHistorico(nameof(Produto), produtoExistente.Id, AcaoHistorico.Alteracao, detalhes);

        return true;
    }
}