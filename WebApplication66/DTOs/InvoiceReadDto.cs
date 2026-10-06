namespace InvoiceManagement.API.DTOs
{
    public class InvoiceReadDto
    {
        public string Number { get; set; }
        public string ClientName { get; set; }
        public decimal Total { get; set; }
    }
}
