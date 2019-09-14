using System;
using Contoso.Phoenix.Common.Configuration.Abstractions;
using Contoso.Phoenix.Data.Common;
using Contoso.Phoenix.Data.Common.Configuration;
using Contoso.Phoenix.Data.Entity.Common;
using Contoso.Phoenix.Data.Xml;
using Contoso.Phoenix.Logic.Instrumentation;
using SimpleInjector;

namespace Contoso.Phoenix.Logic
{
    public class RepositoryDataFactory<TEntity, TKey> : IRepositoryDataFactory<TEntity, TKey>
        where TEntity : IEntity<TKey>, new()
    {
        private readonly Container _container;
        private readonly IOptionsAccessor<DataSourceConfig> _config;
        private readonly IPhoenixLogger _logger;

        public RepositoryDataFactory(Container container, IOptionsAccessor<DataSourceConfig> config, IPhoenixLogger logger)
        {
            _container = container;
            _config = config;
            _logger = logger;
        }

        public IRepository<TEntity, TKey> Get()
        {
            try
            {
                // ReSharper disable once SwitchStatementMissingSomeCases
                switch (_config.Value.DataSource)
                {
                    case "Xml":
                        return _container.GetInstance<XmlRepository<TEntity, TKey>>();
                }
            }
            catch (Exception ex)
            {
                _logger.ErrorInResolvingRepository(Guid.NewGuid(), _config.Value.DataSource, typeof(TEntity).Name, ex);
            }

            return null;
        }
    }
}
