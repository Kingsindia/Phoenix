using Contoso.Phoenix.Common.Configuration.Abstractions;
using Microsoft.Extensions.Configuration;
using SimpleInjector;

namespace Contoso.Phoenix.Common.Configuration.SimpleInjector
{
    public static class SimpleInjectorExtensions
    {
        public static void AddOptions<TOptions>(this Container container, IConfiguration configuration)
            where TOptions : class, new()
        {
            container.RegisterInstance<IOptionsAccessor<TOptions>>(new SimpleInjectorOptionsAccessor<TOptions>(configuration));
        }
    }
}
