using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Contoso.Phoenix.Data.Entity.Model.V1;

namespace Contoso.Phoenix.Logic
{
    public interface IEmployeeLogic
    {
        Task<IList<Employee>> GetAllAsync();

        Task<Employee> GetAsync(int employeeId);

        Task<IList<Employee>> FindAsync(Expression<Func<Employee, bool>> predicate);

        Task AddAsync(Employee employee);

        Task UpdateAsync(Employee employee);

        Task RemoveAsync(int employeeId);
    }
}
