using Entidades;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Repositorios.Generico
{
    public abstract class RepositorioGenerico<TEntity> : IRepositorioGenerico<TEntity> where TEntity : class, IEntity
    {
        protected readonly Contexto.Contexto _contexto;
        protected DbSet<TEntity> DbSet => _contexto.Set<TEntity>();

        public RepositorioGenerico(Contexto.Contexto contexto)
        {
            _contexto = contexto;
        }

        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            return await DbSet.AsNoTracking().ToListAsync().ConfigureAwait(false);
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await DbSet.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id).ConfigureAwait(false);
        }

        public virtual IQueryable<TEntity> GetByCondition(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public virtual async Task<TEntity> CreatedAsync(TEntity entity, bool saveChanges = true)
        {
            await DbSet.AddAsync(entity).ConfigureAwait(false);
            if (saveChanges)
                await _contexto.SaveChangesAsync().ConfigureAwait(false);
            return entity;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity, bool saveChanges = true)
        {
            DbSet.Update(entity);
            if (saveChanges)
                await _contexto.SaveChangesAsync().ConfigureAwait(false);
            return entity;
        }

        public virtual async Task DeleteAsync(Guid id, bool saveChanges = true)
        {
            var entity = await DbSet.FirstOrDefaultAsync(e => e.Id == id).ConfigureAwait(false);
            if (entity != null)
            {
                DbSet.Remove(entity);
                if (saveChanges)
                    await _contexto.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        public virtual async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, bool saveChanges = true)
        {
            var entity = await DbSet.FirstOrDefaultAsync(predicate).ConfigureAwait(false);
            if (entity != null)
            {
                DbSet.Remove(entity);
                if (saveChanges)
                    await _contexto.SaveChangesAsync().ConfigureAwait(false);
            }
        }
    }
}