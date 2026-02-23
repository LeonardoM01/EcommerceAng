using Ecommerce.API.Enums;

namespace Ecommerce.API.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public List<ItemPedido> Itens { get; set; }
        public DateTime DataPedido { get; set; }
        public StatusPedido Status { get; set; }
        public decimal ValorTotal => Itens?.Sum(i => i.Quantidade * i.PrecoUnitario) ?? 0;
    }
}