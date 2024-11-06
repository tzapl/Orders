namespace Orders.Models
{
    public class OrderRequest
    {
        public string ProductId { get; set; } = String.Empty;
        public int Quantity { get; set; }
    }
}
