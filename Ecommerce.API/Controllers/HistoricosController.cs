using Ecommerce.API.Models;
using Ecommerce.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HistoricosController : ControllerBase
{
    private readonly IHistoricoService _historicoService;

    public HistoricosController(IHistoricoService historicoService)
    {
        _historicoService = historicoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Historico>>> GetHistoricos()
    {
        var historicos = await _historicoService.ObterTodosHistoricosAsync();
        return Ok(historicos);
    }    
}
