using System.Linq.Expressions;

namespace Repositorios.Generico
{
    public interface IRepositorioGenerico<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(Guid id);
        Task<TEntity> CreatedAsync(TEntity entity, bool saveChanges = true);
        Task<TEntity> UpdateAsync(TEntity entity, bool saveChanges = true);
        Task DeleteAsync(Guid id, bool saveChanges = true);
        Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, bool saveChanges = true);
    }
}
