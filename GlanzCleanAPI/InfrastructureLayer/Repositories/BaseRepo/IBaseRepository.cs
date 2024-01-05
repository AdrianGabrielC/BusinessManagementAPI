using System.Linq.Expressions;

namespace GlanzCleanAPI.InfrastructureLayer.Repositories.BaseRepo
{
    public interface IRepositoryBase<T>
    {
        // Retrieves all entities of type T.
        IQueryable<T> FindAll(bool trackChanges);

        // Retrieves entities of type T based on the provided condition.
        // Takes an expression parameter representing the condition.
        // Returns an IQueryable<T> representing the filtered collection of entities.
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}
