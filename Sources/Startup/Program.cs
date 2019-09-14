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
        private static IEmployeeLogic _employeeLogic;

        internal static void Main(string[] args)
        {
            // Startup..
            RunAsync().Wait();
        }

        internal static async Task RunAsync()
        {
            var container = SetupDependencyContainer();

            _employeeLogic = container.GetInstance<IEmployeeLogic>();

            /* Workflow: Will explain/discuss more during discussion */

            Console.WriteLine($"Employee Count {await GetCount()}");

            await _employeeLogic.AddAsync(new Employee { Id = 1, Age = 32, Name = "Rajarajan" });

            Console.WriteLine($"Employee Count {await GetCount()}");

            await _employeeLogic.AddAsync(new Employee { Id = 2, Age = 33, Name = "Selva" });

            Console.WriteLine($"Employee Count {await GetCount()}");

            await _employeeLogic.RemoveAsync(5);

            Console.WriteLine($"Employee Count {await GetCount()}");

            await _employeeLogic.AddAsync(new Employee { Id = 7, Age = 33, Name = "Selva", Address = new Address { DoorNumber = "248", Street = "East street", State = "TN", Town = "Salem" } });

            Console.WriteLine($"Employee Count {await GetCount()}");

            var employee = await _employeeLogic.GetAsync(1);
            employee.Designation = "Sr. Advanced Cloud Developer";

            await _employeeLogic.UpdateAsync(employee);

            await _employeeLogic.RemoveAsync(7);

            Console.WriteLine($"Employee Count {await GetCount()}");

            // ... and more!
            // Thank you.
        }

        private static async Task<int?> GetCount()
        {
            return (await _employeeLogic.GetAllAsync())?.Count;
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
            container.RegisterSingleton<IRepositoryDataFactory<Employee, int>, RepositoryDataFactory<Employee, int>>();
            container.RegisterSingleton<IRepository<Employee, int>, XmlRepository<Employee, int>>();

            container.RegisterSingleton<IEmployeeLogic, EmployeeLogic>();

            container.Verify();

            return container;
        }
    }
}
