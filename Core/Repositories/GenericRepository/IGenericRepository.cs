﻿using System.Linq.Expressions;

namespace Core.Repositories.GenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IReadOnlyList<T>> GetAllAsync<Tprop>(int pageNum, int takeParam, Expression<Func<T, Tprop>> navProp = null);
        Task<IReadOnlyList<T>> GetAllAsync(int pageNum, int takeParam, string navProp = null);
        Task<T> GetByIdAsync(Guid id);
    }
}
