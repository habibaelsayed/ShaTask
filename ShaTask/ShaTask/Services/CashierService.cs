using ShaTask.DTOs;
using ShaTask.Interfaces;
using ShaTask.Models;

namespace ShaTask.Services
{
    public class CashierService : ICashierService
    {
        private ICashier cashierRepo;
        public CashierService(ICashier cashierRepo)
        {
            this.cashierRepo = cashierRepo;
        }
        public async Task AddCashierAsync(NewCashierDTO cashierDTO)
        {
            var cashier = MappingDTOToCashier(cashierDTO);
            await cashierRepo.AddAsync(cashier);
            SaveAsync();
        }

        public async Task DeleteCashierAsync(int id)
        {
            await cashierRepo.DeleteAsync(id);
            SaveAsync();
        }

        public async Task<List<CashierDTO>> GetAllCashierAsync()
        {
            var cashiers = await cashierRepo.GetAllAsync();
            var cashierDTOs = new List<CashierDTO>();
            foreach (var cashier in cashiers)
            {
                var cashierDTO = MappingCashierToDTO(cashier);
                cashierDTOs.Add(cashierDTO);
            }
            return cashierDTOs;
        }

        public async Task<CashierDTO> GetCashierByIdAsync(int id)
        {
            var cashier = await cashierRepo.GetAsync(id);
            return MappingCashierToDTO(cashier);
        }

        public CashierDTO MappingCashierToDTO(Cashier cashier)
        {
            return new CashierDTO()
            {
                Id = cashier.Id,
                Name = cashier.CashierName,
                BranchId = cashier.BranchId,
                BranchName = cashier.Branch.BranchName
            };

        }

        public Cashier MappingDTOToCashier(NewCashierDTO newCashierDTO)
        {
            return new Cashier()
            {
                CashierName = newCashierDTO.Name,
                BranchId = newCashierDTO.BranchId
            };
        }

        public async Task SaveAsync()
        {
            await cashierRepo.SaveAsync();
        }

        public async Task UpdateCashierAsync(UpdateCashierDTO cashierDTO)
        {
            var cashier = new Cashier()
            {
                Id = cashierDTO.Id,
                CashierName = cashierDTO.Name,
                BranchId = cashierDTO.BranchId
            };
            await cashierRepo.UpdateAsync(cashier);
            SaveAsync();
        }



    }
}
