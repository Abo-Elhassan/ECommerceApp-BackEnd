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

        public async Task<IReadOnlyList<T>> GetAllAsync(int pageNum, int takeParam)
        {
            if (takeParam>20)
            {
                takeParam = 20;
            }
            int skip = takeParam * (pageNum - 1);
            return await _storecontext.Set<T>().Skip(skip).Take(takeParam).ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _storecontext.Set<T>().FindAsync(id);
        }

    }


}
