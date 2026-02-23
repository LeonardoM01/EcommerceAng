using Ecommerce.API.Models;
using Ecommerce.API.Interfaces;
using Ecommerce.API.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PedidosController : ControllerBase
{
    private readonly IPedidoService _pedidoService;

    public PedidosController(IPedidoService pedidoService)
    {
        _pedidoService = pedidoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Pedido>>> GetPedidos([FromQuery] StatusPedido? status)
    {
        if (status.HasValue)
        {
            var pedido = await _pedidoService.ObterPedidosPorStatusAsync(status.Value);
            return Ok(pedido);
        }
        var pedidos = await _pedidoService.ObterTodosPedidosAsync();
        return Ok(pedidos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Pedido>> GetPedido(int id)
    {
        var pedido = await _pedidoService.ObterPedidoPorIdAsync(id);
        if (pedido == null)
            return NotFound();

        return Ok(pedido);
    }

    [HttpPost]
    public async Task<ActionResult<Pedido>> CreatePedido([FromBody] Pedido pedido)
    {
        var novoPedido = await _pedidoService.AdicionarPedidoAsync(pedido);
        return CreatedAtAction(nameof(GetPedido), new { id = novoPedido.Id }, novoPedido);
    }

    [HttpPost("{id}/pagar")]
    public async Task<IActionResult> PagarPedido(int id)
    {
        var pago = await _pedidoService.PagarPedidoAsync(id);
        if (!pago)
            return BadRequest("Não foi possível pagar o pedido. Verifique se o pedido existe e está no status correto.");

        return NoContent();
    }

    [HttpPost("{id}/cancelar")]
    public async Task<IActionResult> CancelarPedido(int id)
    {
        var cancelado = await _pedidoService.CancelarPedidoAsync(id);
        if (!cancelado)
            return BadRequest("Não foi possível cancelar o pedido. Verifique se o pedido existe e está no status correto.");

        return NoContent();
    }
}