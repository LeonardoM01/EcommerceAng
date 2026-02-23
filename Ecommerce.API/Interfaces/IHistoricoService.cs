using System.Runtime.CompilerServices;
using Ecommerce.API.Models;
using Ecommerce.API.Enums;

namespace Ecommerce.API.Interfaces
{
    public interface IHistoricoService
    {
        Task RegistrarHistorico(string entidade, int entidadeId, AcaoHistorico acao, string detalhes);
        Task<IEnumerable<Historico>> ObterTodosHistoricosAsync();
        Task<IEnumerable<Historico>> ObterHistoricosPorEntidadeAsync(string entidade, int entidadeId);
    }
}