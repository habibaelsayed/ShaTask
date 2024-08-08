using ShaTask.Models;

namespace ShaTask.Interfaces
{
    public interface ICashier
    {
        public Task<List<Cashier>> GetAllAsync();
        public Task<Cashier> GetAsync(int id);
        public Task AddAsync(Cashier cashier);
        public Task DeleteAsync(int id);
        public Task SaveAsync();
        public Task UpdateAsync(Cashier cashier);
    }
}
