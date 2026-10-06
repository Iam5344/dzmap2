namespace InvoiceManagement.API.DTOs
{
    public class InvoiceCreateDto
    {
        public string ClientName { get; set; }
        public decimal Total { get; set; }
    }
}
