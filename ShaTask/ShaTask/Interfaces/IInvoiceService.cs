using ShaTask.DTOs;
using ShaTask.Models;

namespace ShaTask.Interfaces
{
    public interface IInvoiceService
    {
        Task<List<InvoiceDataDTO>> GetAllInvoiceDataAsync();
        Task<InvoiceDataDTO> GetInvoiceDataByIdAsync(long id);

        public List<InvoiceItemDTO> GetAllItemsOfOneInvoice(List<InvoiceDetail> invoiceDetails);
        public InvoiceDataDTO MappingInvoiceToDTO(List<InvoiceDetail> invoiceDetails, InvoiceHeader invoiceHeader);
        Task AddInvoiceAsync(InvoiceHeader invoiceHeader);
        Task AddInvoiceDetailAsync(InvoiceDetail invoiceDetail);
        Task MappingDTOToInvoiceAsync(NewInvoiceDTO invoiceDataDTO);
        Task DeleteInvoiceAsync(long id);
        Task UpdateInvoiceAsync(UpdateInvoiceDataDTO invoiceDataDTO);
    }
}
