using Core.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Core.Repositories.GenericRepository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly StoreContext _storecontext;

        public GenericRepository(StoreContext storecontext)
        {
            _storecontext = storecontext;
        }

        public void Add(T entity)
        {
            _storecontext.Set<T>().Add(entity);

        }

        public void Delete(T entity)
        {
            _storecontext.Set<T>().Remove(entity);

        }

        public async Task<IReadOnlyList<T>> GetAllAsync(int pageNum, int takeParam,string navProp=null)
        {
            if (takeParam>20)
            {
                takeParam = 20;
            }
            int skip = takeParam * (pageNum - 1);

            return navProp is null ? await _storecontext.Set<T>().Skip(skip).Take(takeParam).ToListAsync() :
                await _storecontext.Set<T>().Include(navProp).Skip(skip).Take(takeParam).ToListAsync();
        }
        public async Task<IReadOnlyList<T>> GetAllAsync<Tprop>(int pageNum, int takeParam, Expression<Func<T,Tprop>> navProp=null)
        {
            if (takeParam > 20)
            {
                takeParam = 20;
            }
            int skip = takeParam * (pageNum - 1);

            return navProp is null ? await _storecontext.Set<T>().Skip(skip).Take(takeParam).ToListAsync() :
                await _storecontext.Set<T>().Include(navProp).Skip(skip).Take(takeParam).ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _storecontext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByGuidIdAsync(Guid id)
        {
            return await _storecontext.Set<T>().FindAsync(id);
        }

     

        public async Task<T> GetByIntIdAsync(int id)
        {
            return await _storecontext.Set<T>().FindAsync(id);
        }

        public void Update(T entity)
        {
            _storecontext.Set<T>().Update(entity);
           
        }
    }


}
