using Core.Context;
using Microsoft.EntityFrameworkCore;


namespace Core.Repositories.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly StoreContext _storecontext;

        public GenericRepository(StoreContext storecontext)
        {
            _storecontext = storecontext;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _storecontext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _storecontext.Set<T>().FindAsync(id);
        }

    }


}
