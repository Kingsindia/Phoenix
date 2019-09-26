using System;
using System.IO;
using System.Threading.Tasks;
using Contoso.Phoenix.Common.Configuration.SimpleInjector;
using Contoso.Phoenix.Data.Common;
using Contoso.Phoenix.Data.Common.Configuration;
using Contoso.Phoenix.Data.Entity.Model.V1;
using Contoso.Phoenix.Data.Xml;
using Contoso.Phoenix.Data.Xml.Configuration;
using Contoso.Phoenix.Data.Xml.Instrumentation;
using Contoso.Phoenix.Logic;
using Contoso.Phoenix.Logic.Instrumentation;
using Microsoft.Extensions.Configuration;
using Pluralize.NET;
using SimpleInjector;

namespace Contoso.Phoenix.Startup
{
    internal class Program
    {
        private static IPostLogic _postLogic;
        private static IContentLogic _contentLogic;


        internal static void Main(string[] args)
        {
            // Startup..
            RunAsync().Wait();
        }

        internal static async Task RunAsync()
        {
            var container = SetupDependencyContainer();

            _postLogic = container.GetInstance<IPostLogic>();
            _contentLogic = container.GetInstance<IContentLogic>();

            await _postLogic.GetAllAsync();

            Console.WriteLine($"Post Count {await GetCount()}");

            await _postLogic.Create(new Post
            {
                Id = Guid.NewGuid(),
                PostTitle = "MyPost",
                Contents = new System.Collections.Generic.List<Content> { new Content { Id = Guid.NewGuid() } },
                Likes = new System.Collections.Generic.List<LikeInfo> { new LikeInfo { Id = Guid.NewGuid() } }
            });

            Console.WriteLine($"Post Count {await GetCount()}");

            var contentId = Guid.NewGuid();

            // Upload to blob...

            await _contentLogic.Create(new Content {Id = contentId, ContentType = ContentTypeEnum.Image});

            // ... and more!
            // Thank you.
        }

        private static async Task<int?> GetCount()
        {
            return (await _postLogic.GetAllAsync())?.Count;
        }

        private static Container SetupDependencyContainer()
        {
            var container = new Container();

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json", true, true);

            var config = builder.Build();

            container.AddOptions<DataSourceConfig>(config.GetSection(DataSourceConfig.ConfigKey));
            container.AddOptions<XmlDataConfig>(config.GetSection(XmlDataConfig.ConfigKey));

            container.RegisterSingleton<IPhoenixLogger>(() => PhoenixLogger.Instance);
            container.RegisterSingleton<IXmlDataProviderLogger>(() => PhoenixLogger.Instance);

            container.RegisterSingleton<IPluralize, Pluralizer>();
            container.RegisterSingleton<IXmlDataProvider, XmlDataProvider>();
            container.RegisterSingleton<IRepositoryDataFactory<Post, Guid>, RepositoryDataFactory<Post, Guid>>();
            container.RegisterSingleton<IRepository<Post, Guid>, XmlRepository<Post, Guid>>();

            container.RegisterSingleton<IPostLogic, PostLogic>();

            container.RegisterSingleton<IRepositoryDataFactory<Content, Guid>, RepositoryDataFactory<Content, Guid>>();
            container.RegisterSingleton<IRepository<Content, Guid>, XmlRepository<Content, Guid>>();

            container.RegisterSingleton<IContentLogic, ContentLogic>();

            container.Verify();

            return container;
        }
    }
}
