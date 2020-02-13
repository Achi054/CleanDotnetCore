using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repository.Contracts;
using Repository.EntityFramework.Context;

namespace Repository.EntityFramework
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly eComContext _context;

        public RepositoryBase(eComContext context) => _context = context;

        public async Task Create(T entity) => await Task.FromResult(_context.Set<T>().Add(entity));

        public async Task Delete(T entity) => await Task.FromResult(_context.Set<T>().Remove(entity));

        public async Task<IQueryable<T>> FindAll() => await Task.FromResult(_context.Set<T>().AsNoTracking());

        public async Task<IQueryable<T>> FindAllByCondition(Expression<Func<T, bool>> expression)
            => await Task.FromResult(_context.Set<T>().Where(expression).AsNoTracking());

        public async Task Update(T entity) => await Task.FromResult(_context.Set<T>().Update(entity));
    }
}
