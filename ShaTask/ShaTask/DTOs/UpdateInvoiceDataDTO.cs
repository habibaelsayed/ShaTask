namespace ShaTask.DTOs
{
    public class UpdateInvoiceDataDTO
    {
        public long InvoiceHeaderId { get; set; }
        public string CustomerName { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int CashierId { get; set; }
        public List<NewInvoiceItemDTO> InvoiceItems { get; set; }
    }
}
