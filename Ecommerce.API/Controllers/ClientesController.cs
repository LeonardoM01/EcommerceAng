using Ecommerce.API.Models;
using Ecommerce.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly IClienteService _clientService;

    public ClientesController(IClienteService clientService)
    {
        _clientService = clientService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
    {
        var clientes = await _clientService.ObterTodosClientesAsync();
        return Ok(clientes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Cliente>> GetCliente(int id)
    {
        var cliente = await _clientService.ObterClientePorIdAsync(id);
        if (cliente == null)
            return NotFound();

        return Ok(cliente);
    }

    [HttpPost]
    public async Task<ActionResult<Cliente>> CreateCliente(Cliente cliente)
    {
        var novoCliente = await _clientService.AdicionarClienteAsync(cliente);
        return CreatedAtAction(nameof(GetCliente), new { id = novoCliente.Id }, novoCliente);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCliente(int id, Cliente cliente)
    {
        if (id != cliente.Id)
            return BadRequest();

        var atualizado = await _clientService.AtualizarClienteAsync(id, cliente);
        if (!atualizado)
            return NotFound();

        return NoContent();
    }
}
