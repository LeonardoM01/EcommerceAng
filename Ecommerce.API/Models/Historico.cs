using Ecommerce.API.Enums;

namespace Ecommerce.API.Models
{
    public class Historico
    {
        public int Id { get; set; }
        public string Entidade { get; set; }
        public int EntidadeId { get; set; }
        public AcaoHistorico Acao { get; set; }
        public DateTime DataAlteracao { get; set; }
        public string Detalhes { get; set; }
    }
}