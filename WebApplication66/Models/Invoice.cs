namespace InvoiceManagement.API.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string ClientName { get; set; }
        public decimal Total { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
