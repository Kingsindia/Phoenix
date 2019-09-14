using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Contoso.Phoenix.Data.Common;
using Contoso.Phoenix.Data.Entity.Common;
using Contoso.Phoenix.Data.Entity.Common.Exceptions;

namespace Contoso.Phoenix.Data.Xml
{
    public class XmlRepository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : IEntity<TKey>, new()
    {
        private readonly IXmlDataProvider _dataProvider;
        private ICollection<TEntity> _items;

        public XmlRepository(IXmlDataProvider dataProvider)
        {
            _dataProvider = dataProvider;

            Initialize();
        }

        private void Initialize()
        {
            _items = _dataProvider.Load<TEntity>();
        }

        public Task<ICollection<TEntity>> GetAllAsync()
        {
            return Task.FromResult(_items);
        }

        public Task<ICollection<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult<ICollection<TEntity>>(_items?.AsQueryable().Where(predicate).ToList());
        }

        public Task<TEntity> GetAsync(TKey id)
        {
            return Task.FromResult(_items.FirstOrDefault(item => id.Equals(item.Id)));
        }

        public Task<TKey> AddAsync(TEntity model)
        {
            if (_items.Any(i => i.Id.Equals(model.Id)))
            {
                throw new EntityConflictException(typeof(TEntity).Name, "Entity already exists.");
            }

            _items.Add(model);

            return Task.FromResult(model.Id);
        }

        public async Task<bool> UpdateAsync(TEntity model)
        {
            await RemoveAsync(model.Id);

            _items.Add(model);

            return true;
        }

        public async Task<bool> RemoveAsync(TKey id)
        {
            var itemToRemove = await GetAsync(id);

            if (itemToRemove == null)
            {
                // Sample usage of custom exceptions
                throw new EntityNotFoundException(typeof(TEntity).Name);
            }

            return _items.Remove(itemToRemove);
        }

        public Task SaveAsync()
        {
            _dataProvider.Save(_items);

            return Task.CompletedTask;
        }
    }
}
