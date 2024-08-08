using Microsoft.AspNetCore.Http.HttpResults;
using ShaTask.DTOs;
using ShaTask.Interfaces;
using ShaTask.Models;
using ShaTask.Repositories;

namespace ShaTask.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoice invoiceRepo;

        public InvoiceService(IInvoice invoiceRepo)
        {
            this.invoiceRepo = invoiceRepo;
        }
        public async Task<List<InvoiceDataDTO>> GetAllInvoiceDataAsync()
        {
            var invoiceHeaders = await invoiceRepo.GetAllAsync();
            var invoiceDataDTOs = new List<InvoiceDataDTO>();

            foreach (var invoiceHeader in invoiceHeaders)
            {
                var invoiceDetails = await invoiceRepo.GetItemsOfInvoiceAsync(invoiceHeader.Id);

                var invoiceDataDTO = MappingInvoiceToDTO(invoiceDetails, invoiceHeader);
                invoiceDataDTOs.Add(invoiceDataDTO);
            }

            return invoiceDataDTOs;
        }
        public async Task<InvoiceDataDTO> GetInvoiceDataByIdAsync(long id)
        {
            var invoiceHeader = await invoiceRepo.GetAsync(id);
            var invoiceDetails = await invoiceRepo.GetItemsOfInvoiceAsync(invoiceHeader.Id);
            var invoiceDataDTO = MappingInvoiceToDTO(invoiceDetails, invoiceHeader);

            return invoiceDataDTO;
        }

        public List<InvoiceItemDTO> GetAllItemsOfOneInvoice(List<InvoiceDetail> invoiceDetails)
        {
            return invoiceDetails.Select(detail => new InvoiceItemDTO
            {
                ItemName = detail.ItemName,
                ItemPrice = detail.ItemPrice,
                ItemCount = detail.ItemCount,
                TotalPriceItem = detail.ItemPrice * detail.ItemCount
            }).ToList();
        }


        public InvoiceDataDTO MappingInvoiceToDTO(List<InvoiceDetail> invoiceDetails, InvoiceHeader invoiceHeader)
        {
            var invoiceItemDTOs = GetAllItemsOfOneInvoice(invoiceDetails);

            var totalPrice = invoiceItemDTOs.Sum(item => item.TotalPriceItem);

            var invoiceDataDTO = new InvoiceDataDTO
            {
                InvoiceHeaderId = invoiceHeader.Id,
                CustomerName = invoiceHeader.CustomerName,
                InvoiceDate = invoiceHeader.Invoicedate,
                CashierId = (int)invoiceHeader.CashierId,
                CashierName = invoiceHeader.Cashier.CashierName,
                BranchId = (int)invoiceHeader.BranchId,
                BranchName = invoiceHeader.Branch.BranchName,
                InvoiceItems = invoiceItemDTOs,
                TotalPrice = totalPrice
            };

            return invoiceDataDTO;

        }

        public async Task AddInvoiceAsync(InvoiceHeader invoiceHeader)
        {
            await invoiceRepo.AddAsync(invoiceHeader);
        }

        public async Task AddInvoiceDetailAsync(InvoiceDetail invoiceDetail)
        {
            await invoiceRepo.AddInvoiceDetail(invoiceDetail);
        }
        public async Task MappingDTOToInvoiceAsync(NewInvoiceDTO invoiceDataDTO)
        {
            // Step 1: Get the branch ID using Cashier ID
            var branchId = await invoiceRepo.GetBranchIdByCashierId(invoiceDataDTO.CashierId);

            // Step 2: Map DTO to InvoiceHeader entity
            var invoiceHeader = new InvoiceHeader()
            {
                CustomerName = invoiceDataDTO.CustomerName,
                Invoicedate = DateTime.Now,
                CashierId = invoiceDataDTO.CashierId,
                BranchId = branchId
            };

            // Step 3: Save InvoiceHeader to the database
            await invoiceRepo.AddAsync(invoiceHeader);

            // Step 4: Get the saved InvoiceHeader's ID
            var lastAddedInvoice = await invoiceRepo.GetLastAddedInvoiceHeaderAsync();

            // Step 5: Iterate through each item in the DTO and save to InvoiceDetail
            foreach (var invoiceItemDTO in invoiceDataDTO.InvoiceItems)
            {
                var invoiceDetail = new InvoiceDetail()
                {
                    InvoiceHeaderId = lastAddedInvoice.Id,
                    ItemName = invoiceItemDTO.ItemName,
                    ItemCount = invoiceItemDTO.ItemCount,
                    ItemPrice = invoiceItemDTO.ItemPrice
                };

                // Save each InvoiceDetail
                await invoiceRepo.AddInvoiceDetail(invoiceDetail);
            }
        }

        public async Task DeleteInvoiceAsync(long id)
        {
            await invoiceRepo.DeleteAsync(id);
        }

        public async Task UpdateInvoiceAsync(UpdateInvoiceDataDTO invoiceDataDTO)
        {
            var invoiceHeader = await invoiceRepo.GetAsync(invoiceDataDTO.InvoiceHeaderId);
            invoiceHeader.Invoicedate = invoiceDataDTO.InvoiceDate;
            invoiceHeader.CustomerName = invoiceDataDTO.CustomerName;
            invoiceHeader.CashierId = invoiceDataDTO.CashierId;

            await invoiceRepo.UpdateInvoiceData(invoiceHeader);

            await invoiceRepo.DeleteInvoiceDetailByHeader(invoiceHeader.Id);

            foreach(NewInvoiceItemDTO invoiceItemDTO in invoiceDataDTO.InvoiceItems)
            {
                var invoiceItem = new InvoiceDetail()
                {
                    ItemName = invoiceItemDTO.ItemName,
                    ItemCount = invoiceItemDTO.ItemCount,
                    ItemPrice = invoiceItemDTO.ItemPrice,
                    InvoiceHeaderId = invoiceHeader.Id
                };

                await invoiceRepo.AddInvoiceDetail(invoiceItem);
            }
        }
    }
}
