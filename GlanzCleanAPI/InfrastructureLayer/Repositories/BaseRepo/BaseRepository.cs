using System.Linq.Expressions;
using System;
using GlanzCleanAPI.InfrastructureLayer.DbContext;
using Microsoft.EntityFrameworkCore;

namespace GlanzCleanAPI.InfrastructureLayer.Repositories.BaseRepo
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected GlanzCleanDbContext _context;

        public RepositoryBase(GlanzCleanDbContext context)
        {
            _context = context;
        }
        // Retrieves all entities of type T.
        public IQueryable<T> FindAll(bool trackChanges) => 
            !trackChanges ? _context.Set<T>().AsNoTracking() : _context.Set<T>();


        // Retrieves entities of type T based on the provided condition.
        // Takes an expression parameter representing the condition.
        // Returns an IQueryable<T> representing the filtered collection of entities.
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) => 
            !trackChanges ? _context.Set<T>().Where(expression).AsNoTracking() : _context.Set<T>().Where(expression);
 
        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

    }
}
