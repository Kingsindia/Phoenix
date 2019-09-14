using System.Collections.Generic;
using Contoso.Phoenix.Common.Configuration.SimpleInjector;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Contoso.Phoenix.Tests.Common.Tests.Configuration
{
    public class SimpleInjectorOptionsTests
    {
        public class SimpleInjectorOptionsTest
        {
            [Fact]
            public void EnsureOptionsLoaded()
            {
                var config1 = new Dictionary<string, string>
                {
                    {"Section:Key1", "Value1"}
                };

                var configuration = new ConfigurationBuilder()
                    .AddInMemoryCollection(config1)
                    .Build();

                var options = new SimpleInjectorOptionsAccessor<Helpers.Configuration>(configuration);

                Assert.Equal("Value1", options.Value.Section.Key1);
            }
        }
    }
}
