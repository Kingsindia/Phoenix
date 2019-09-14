using System;
using Contoso.Phoenix.Common.Configuration.Abstractions;
using Microsoft.Extensions.Configuration;

namespace Contoso.Phoenix.Common.Configuration.SimpleInjector
{
    public class SimpleInjectorOptionsAccessor<TOptions> : IOptionsAccessor<TOptions>
        where TOptions : class, new()
    {
        private readonly IConfiguration _config;

        private volatile Lazy<TOptions> _options;

        public SimpleInjectorOptionsAccessor(IConfiguration config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));

            _options = new Lazy<TOptions>(LoadConfig);
        }

        public TOptions Value => _options.Value;

        private TOptions LoadConfig()
        {
            var options = new TOptions();

            _config.Bind(options);

            return options;
        }
    }
}
