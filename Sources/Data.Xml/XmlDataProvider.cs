using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using Contoso.Phoenix.Common.Configuration.Abstractions;
using Contoso.Phoenix.Data.Entity.Common;
using Contoso.Phoenix.Data.Xml.Configuration;
using Contoso.Phoenix.Data.Xml.Instrumentation;
using Pluralize.NET;

namespace Contoso.Phoenix.Data.Xml
{
    public class XmlDataProvider : IXmlDataProvider
    {
        private readonly IOptionsAccessor<XmlDataConfig> _xmlDataConfig;
        private readonly IPluralize _pluralizer;
        private readonly IXmlDataProviderLogger _logger;

        public XmlDataProvider(IOptionsAccessor<XmlDataConfig> xmlDataConfig, IPluralize pluralizer, IXmlDataProviderLogger logger)
        {
            _xmlDataConfig = xmlDataConfig;
            _pluralizer = pluralizer;
            _logger = logger;
        }

        public ICollection<TEntity> Load<TEntity>()
        {
            var items = new List<TEntity>();

            try
            {
                var options = GetXmlDataOptions<TEntity>();

                using (var reader = new StreamReader(new FileStream(options.Path, FileMode.Open)))
                {
                    var xmlRoot = new XmlRootAttribute { ElementName = options.RootElementName, IsNullable = true };
                    var serializer = new XmlSerializer(typeof(List<TEntity>), xmlRoot);
                    items = (List<TEntity>)serializer.Deserialize(reader);
                }

                return items;
            }
            catch (Exception ex)
            {
                _logger.DeserializationError(Guid.NewGuid(), typeof(TEntity).Name, ex);

                return items;
            }
        }

        public bool Save<TEntity>(ICollection<TEntity> entities)
        {
            try
            {
                var options = GetXmlDataOptions<TEntity>();

                EnsureFilePathAvailable(options);

                using (var writer = new StreamWriter(options.Path, false))
                {
                    var xmlRoot = new XmlRootAttribute { ElementName = options.RootElementName, IsNullable = true };
                    var serializer = new XmlSerializer(typeof(List<TEntity>), xmlRoot);
                    serializer.Serialize(writer, entities);
                    writer.Close();
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.SerializationError(Guid.NewGuid(), typeof(TEntity).Name, ex);

                return false;
            }
        }

        private XmlDataOptions GetXmlDataOptions<TEntity>()
        {
            var entityName = typeof(TEntity).GetCustomAttribute<EntityNameAttribute>()?.Value ?? typeof(TEntity).Name;

            return new XmlDataOptions
            {
                RootElementName = _pluralizer.Pluralize(entityName),
                EntityName = entityName,
                Path = Path.Combine(_xmlDataConfig.Value.StoragePath, $"{entityName}.xml")
            };
        }

        private void EnsureFilePathAvailable(XmlDataOptions options)
        {
            if (!Directory.Exists(_xmlDataConfig.Value.StoragePath))
            {
                Directory.CreateDirectory(_xmlDataConfig.Value.StoragePath);
            }

            if (!File.Exists(options.Path))
            {
                File.Create(options.Path);
            }
        }
    }
}
