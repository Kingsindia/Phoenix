using Contoso.Phoenix.Common.Configuration.Abstractions;
using Contoso.Phoenix.Data.Common;
using Contoso.Phoenix.Data.Common.Configuration;
using Contoso.Phoenix.Data.Entity.Common;

namespace Contoso.Phoenix.Logic
{
    public interface IRepositoryDataFactory<TEntity, TKey>
        where TEntity : IEntity<TKey>
    {
        IRepository<TEntity, TKey> Get();
    }
}
