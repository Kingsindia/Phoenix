using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Contoso.Phoenix.Data.Entity.Common;

namespace Contoso.Phoenix.Data.Common
{
    public interface IRepository<TEntity, TKey>
        where TEntity : IEntity<TKey>
    {
        Task<ICollection<TEntity>> GetAllAsync();

        Task<ICollection<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> GetAsync(TKey id);

        Task<TKey> AddAsync(TEntity model);

        Task<bool> UpdateAsync(TEntity model);

        Task<bool> RemoveAsync(TKey id);

        Task SaveAsync();
    }
}
