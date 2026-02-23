using Ecommerce.API.Models;
using Ecommerce.API.Interfaces;
using Ecommerce.API.Enums;
using System.Runtime.CompilerServices;

namespace Ecommerce.API.Services;

public class HistoricoService : IHistoricoService
{
    private readonly List<Historico> _historicos = new List<Historico>();
    private int _nextId = 1;

    public Task<IEnumerable<Historico>> ObterTodosHistoricosAsync()
    {
        return Task.FromResult(_historicos.AsEnumerable());
    }

    public Task RegistrarHistorico(string entidade, int entidadeId, AcaoHistorico acao, string detalhes)
    {
        var historico = new Historico
        {
            Id = _historicos.Count + 1,
            Entidade = entidade,
            EntidadeId = entidadeId,
            Acao = acao,
            Detalhes = detalhes,
            DataAlteracao = DateTime.UtcNow
        };

        _historicos.Add(historico);

        return Task.CompletedTask;
    }

    public Task<IEnumerable<Historico>> ObterHistoricosPorEntidadeAsync(string entidade, int entidadeId)
    {
        var historicos = _historicos.Where(h => h.Entidade == entidade && h.EntidadeId == entidadeId);
        return Task.FromResult(historicos.AsEnumerable());
    }
}