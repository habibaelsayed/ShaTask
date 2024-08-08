using ShaTask.DTOs;
using ShaTask.Models;

namespace ShaTask.Interfaces
{
    public interface IInvoice
    {
        public Task<List<InvoiceHeader>> GetAllAsync();
        public Task<InvoiceHeader> GetAsync(long id);
        public Task AddAsync(InvoiceHeader invoice);
        public Task DeleteAsync(long id);
        public Task<List<InvoiceDetail>> GetItemsOfInvoiceAsync(long id);
        public Task AddInvoiceDetail(InvoiceDetail invoiceDetail);
        public Task SaveAsync();
        public Task<InvoiceHeader> GetLastAddedInvoiceHeaderAsync();
        public Task<int> GetBranchIdByCashierId(int id);
        public Task DeleteInvoiceDetailByHeader(long id);
        public Task UpdateInvoiceData(InvoiceHeader invoiceHeader);

    }
}
