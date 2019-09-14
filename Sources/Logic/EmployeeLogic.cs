using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Contoso.Phoenix.Data.Entity.Model.V1;
using Contoso.Phoenix.Logic.Instrumentation;

namespace Contoso.Phoenix.Logic
{
    public class EmployeeLogic : IEmployeeLogic
    {
        private readonly IRepositoryDataFactory<Employee, int> _dataFactory;
        private readonly IPhoenixLogger _logger;

        public EmployeeLogic(IRepositoryDataFactory<Employee, int> dataFactory, IPhoenixLogger logger)
        {
            _dataFactory = dataFactory;
            _logger = logger;
        }

        public async Task<IList<Employee>> GetAllAsync()
        {
            var repository = _dataFactory.Get();

            var employees = await repository.GetAllAsync();

            return employees?.ToList();
        }

        public async Task<Employee> GetAsync(int employeeId)
        {
            var repository = _dataFactory.Get();

            var employee = await repository.GetAsync(employeeId);

            return employee;
        }

        public async Task<IList<Employee>> FindAsync(Expression<Func<Employee, bool>> predicate)
        {
            var repository = _dataFactory.Get();

            var employees = await repository.FindAsync(predicate);

            return employees?.ToList();
        }

        public async Task AddAsync(Employee employee)
        {
            try
            {
                var repository = _dataFactory.Get();

                await repository.AddAsync(employee);

                await repository.SaveAsync();

                _logger.NewEmployeeCreated(Guid.NewGuid(), employee.Id);
            }
            catch (Exception ex)
            {
                _logger.ErrorInCreatingEmployee(Guid.NewGuid(), employee.Id, ex);
            }
        }

        public async Task UpdateAsync(Employee employee)
        {
            try
            {
                var repository = _dataFactory.Get();

                await repository.UpdateAsync(employee);

                await repository.SaveAsync();

                _logger.EmployeeUpdated(Guid.NewGuid(), employee.Id);
            }
            catch (Exception ex)
            {
                _logger.ErrorInUpdatingEmployee(Guid.NewGuid(), employee.Id, ex);
            }
        }

        public async Task RemoveAsync(int employeeId)
        {
            try
            {
                var repository = _dataFactory.Get();

                await repository.RemoveAsync(employeeId);

                await repository.SaveAsync();

                _logger.EmployeeRemoved(Guid.NewGuid(), employeeId);
            }
            catch (Exception ex)
            {
                _logger.ErrorInRemovingEmployee(Guid.NewGuid(), employeeId, ex);
            }
        }
    }
}
