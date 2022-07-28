using System.Linq.Expressions;

namespace Core.Repositories.GenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IReadOnlyList<T>> GetAllAsync<Tprop>(int pageNum, int takeParam, Expression<Func<T, Tprop>> navProp = null);
        Task<IReadOnlyList<T>> GetAllAsync(int pageNum, int takeParam, string navProp = null);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByGuidIdAsync(Guid id);
        Task<T> GetByIntIdAsync(int id);

        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);



    }
}
