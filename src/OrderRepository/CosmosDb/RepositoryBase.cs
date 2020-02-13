using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Cosmonaut;
using Repository.Contracts;

namespace Repository.CosmosDb
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly ICosmosStore<T> _context;

        public RepositoryBase(ICosmosStore<T> cosmosStore)
            => _context = cosmosStore;

        public async Task Create(T entity)
            => await _context.AddAsync(entity);

        public async Task Delete(T entity)
            => await _context.RemoveAsync(entity);

        public async Task<IQueryable<T>> FindAll()
            => await Task.FromResult(_context.Query());

        public async Task<IQueryable<T>> FindAllByCondition(Expression<Func<T, bool>> expression)
            => await Task.FromResult(_context.Query().Where(expression));

        public async Task Update(T entity)
            => await _context.UpdateAsync(entity);
    }
}
