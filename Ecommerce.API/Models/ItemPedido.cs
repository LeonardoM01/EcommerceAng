namespace Ecommerce.API.Models
{
    public class ItemPedido
    {
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
    }
}