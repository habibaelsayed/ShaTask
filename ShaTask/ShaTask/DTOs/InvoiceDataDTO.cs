using System.Diagnostics.Eventing.Reader;

namespace ShaTask.DTOs
{
    public class InvoiceDataDTO
    {
        public long InvoiceHeaderId { get; set; }
        public string CustomerName { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int CashierId { get; set; }
        public string? CashierName { get; set; }
        public int BranchId { get; set; }
        public string? BranchName { get; set; }
        public List<InvoiceItemDTO> InvoiceItems { get; set; }
        public double TotalPrice { get; set; }
    }
}
