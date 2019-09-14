using System;
using Contoso.Phoenix.Data.Xml.Instrumentation;

namespace Contoso.Phoenix.Logic.Instrumentation
{
    public interface IPhoenixLogger : IXmlDataProviderLogger
    {
        void NewEmployeeCreated(Guid activityId, int employeeId);

        void ErrorInCreatingEmployee(Guid activityId, int employeeId, Exception exception);

        void EmployeeRemoved(Guid activityId, int employeeId);

        void ErrorInRemovingEmployee(Guid activityId, int employeeId, Exception exception);

        void EmployeeUpdated(Guid activityId, int employeeId);

        void ErrorInUpdatingEmployee(Guid activityId, int employeeId, Exception exception);

        void ErrorInResolvingRepository(Guid activityId, string dataSourceName, string typeName, Exception exception);
    }
}
