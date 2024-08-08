using Microsoft.EntityFrameworkCore;
using ShaTask.DTOs;
using ShaTask.Interfaces;
using ShaTask.Models;

namespace ShaTask.Repositories
{
    public class InvoiceRepo : IInvoice
    {
        private ShaTaskContext context;
        public InvoiceRepo(ShaTaskContext context)
        {

            this.context = context;

        }
        public async Task AddAsync(InvoiceHeader invoice)
        {
            await context.InvoiceHeaders.AddAsync(invoice);
            await SaveAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var invoice = await context.InvoiceHeaders.FindAsync(id);
            if (invoice != null)
            {
                context.InvoiceHeaders.Remove(invoice);
                await SaveAsync();
            }
        }

        public async Task<InvoiceHeader> GetAsync(long id)
        {
            return await context.InvoiceHeaders.FindAsync(id);
        }

        public async Task<List<InvoiceHeader>> GetAllAsync()
        {
            return await context.InvoiceHeaders.ToListAsync();
        }

        public async Task<List<InvoiceDetail>> GetItemsOfInvoiceAsync(long id)
        {
            return await context.InvoiceDetails
                               .Where(invoiceItem => invoiceItem.InvoiceHeaderId == id)
                               .ToListAsync();
        }


        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task AddInvoiceDetail(InvoiceDetail invoiceDetail)
        {
            await context.InvoiceDetails.AddAsync(invoiceDetail);
            await SaveAsync();
        }

        public async Task<InvoiceHeader> GetLastAddedInvoiceHeaderAsync()
        {
            return await context.InvoiceHeaders
                                 .OrderByDescending(i => i.Id)
                                 .FirstOrDefaultAsync();
        }

        public async Task<int> GetBranchIdByCashierId(int id)
        {
            var cashier = await context.Cashiers.FindAsync(id);

            return cashier.BranchId;
        }

        public async Task DeleteInvoiceDetailByHeader(long id)
        {
            var invoiceDetails = context.InvoiceDetails.Where(item => item.InvoiceHeaderId == id);

            context.InvoiceDetails.RemoveRange(invoiceDetails);

            await SaveAsync();
        }

        public async Task UpdateInvoiceData(InvoiceHeader invoiceHeader)
        {
            context.InvoiceHeaders.Update(invoiceHeader);
            await SaveAsync();
        }
    }
}
