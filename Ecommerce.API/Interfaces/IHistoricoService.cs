using System.Runtime.CompilerServices;
using Ecommerce.API.Models;

namespace Ecommerce.API.Interfaces
{
    public interface IHistoricoService
    {
        Task<AsyncVoidMethodBuilder> RegistrarHistoricoAsync(int entidade, int entidadeId, AcaoHistorico acao, string detalhes);
        Task<IEnumerable<Historico>> ObterTodosHistoricosAsync();
        Task<IEnumerable<Historico>> ObterHistoricosPorEntidadeAsync(string entidade, int entidadeId);
    }
}