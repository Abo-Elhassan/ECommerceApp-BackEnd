namespace Core.Repositories.GenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IReadOnlyList<T>> GetAllAsync(int pageNum, int takeParam);
        Task<T> GetByIdAsync(Guid id);
    }
}
