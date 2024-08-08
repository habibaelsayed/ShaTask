using Microsoft.EntityFrameworkCore;
using ShaTask.Interfaces;
using ShaTask.Models;

namespace ShaTask.Repositories
{
    public class CashierRepo : ICashier
    {
        private ShaTaskContext context;
        public CashierRepo(ShaTaskContext context)
        {
            this.context = context;
        }
        public async Task AddAsync(Cashier cashier)
        {
            await context.Cashiers.AddAsync(cashier);
            await SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var cashier = await context.Cashiers.FindAsync(id);
            if (cashier != null)
            {
                context.Cashiers.Remove(cashier);
                await SaveAsync();
            }
        }

        public async Task<List<Cashier>> GetAllAsync()
        {
            return await context.Cashiers.ToListAsync();
        }

        public async Task<Cashier> GetAsync(int id)
        {
            return await context.Cashiers.FindAsync(id);
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Cashier cashier)
        {
            context.Cashiers.Update(cashier);
            await SaveAsync();
        }

    }
}
