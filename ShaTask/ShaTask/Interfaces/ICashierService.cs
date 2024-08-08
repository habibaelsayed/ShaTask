using ShaTask.DTOs;
using ShaTask.Models;

namespace ShaTask.Interfaces
{
    public interface ICashierService
    {
        Task<List<CashierDTO>> GetAllCashierAsync();
        Task<CashierDTO> GetCashierByIdAsync(int id);
        Task AddCashierAsync(NewCashierDTO cashierDTO);
        Task DeleteCashierAsync(int id);
        Task UpdateCashierAsync(UpdateCashierDTO cashierDTO);
        Task SaveAsync();
        public CashierDTO MappingCashierToDTO(Cashier cashier);
        public Cashier MappingDTOToCashier(NewCashierDTO newCashierDTO);
    }
}
