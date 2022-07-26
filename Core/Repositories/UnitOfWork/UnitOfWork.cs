using Core.Context;
using Core.Repositories.GenericRepository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {


        private readonly StoreContext _context;

        //any repo that we use inside this unit of work are going to be stored inside this hash table.
        private Hashtable _repositories;
        public UnitOfWork(StoreContext context)
        {
            _context = context;
        }
        // Now we can have a single repository or we could have 100 repositories and
        // we can collect them and store them in the hashtable

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();

        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            //check if no repo so we create new hashtable
            if (_repositories == null) _repositories = new Hashtable();

            var type = typeof(TEntity).Name;

            //check if repo doesn't added then we create it by entity name in hashtable
            if (!_repositories.ContainsKey(type))
            {

                var repositoryType = typeof(GenericRepository<>);                                           //use context to pass it to unit of work
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);

                _repositories.Add(type, repositoryInstance);
            }

            return (IGenericRepository<TEntity>)_repositories[type];
        }
    }
}
