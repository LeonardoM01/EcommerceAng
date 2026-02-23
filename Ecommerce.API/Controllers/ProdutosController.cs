using Ecommerce.API.Models;
using Ecommerce.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly IProdutoService _produtoService;

    public ProdutosController(IProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
    {
        var produtos = await _produtoService.ObterTodosProdutosAsync();
        return Ok(produtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Produto>> GetProduto(int id)
    {
        var produto = await _produtoService.ObterProdutoPorIdAsync(id);
        if (produto == null)
            return NotFound();

        return Ok(produto);
    }

    [HttpPost]
    public async Task<ActionResult<Produto>> CreateProduto([FromBody] Produto produto)
    {
        var novoProduto = await _produtoService.AdicionarProdutoAsync(produto);
        return CreatedAtAction(nameof(GetProduto), new { id = novoProduto.Id }, novoProduto);
    }
}
