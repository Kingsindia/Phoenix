using System;

namespace Contoso.Phoenix.Logic.Instrumentation
{
    /// <summary>
    /// Simple console logger for demonstration. In reality, we would go with semantic logging.
    /// </summary>
    public sealed class PhoenixLogger : IPhoenixLogger
    {
        public static PhoenixLogger Instance = new PhoenixLogger();

        private PhoenixLogger()
        {
        }

        public void SerializationError(Guid activityId, string typeName, Exception exception)
        {
            Console.WriteLine($"SerializationError: activityId {activityId}, typeName {typeName}, exception {exception.GetBaseException().Message}");
        }

        public void DeserializationError(Guid activityId, string typeName, Exception exception)
        {
            Console.WriteLine($"DeserializationError: activityId {activityId}, typeName {typeName}, exception {exception.GetBaseException().Message}");
        }

        public void NewEmployeeCreated(Guid activityId, int employeeId)
        {
            Console.WriteLine($"NewEmployeeCreated : activityId {activityId}, employeeId {employeeId}");
        }

        public void ErrorInCreatingEmployee(Guid activityId, int employeeId, Exception exception)
        {
            Console.WriteLine($"ErrorInCreatingEmployee: activityId {activityId}, employeeId {employeeId}, exception {exception.GetBaseException().Message}");
        }

        public void EmployeeRemoved(Guid activityId, int employeeId)
        {
            Console.WriteLine($"EmployeeRemoved : activityId {activityId}, employeeId {employeeId}");
        }

        public void ErrorInRemovingEmployee(Guid activityId, int employeeId, Exception exception)
        {
            Console.WriteLine($"ErrorInRemovingEmployee: activityId {activityId}, employeeId {employeeId}, exception {exception.GetBaseException().Message}");
        }

        public void EmployeeUpdated(Guid activityId, int employeeId)
        {
            Console.WriteLine($"EmployeeUpdated : activityId {activityId}, employeeId {employeeId}");
        }

        public void ErrorInUpdatingEmployee(Guid activityId, int employeeId, Exception exception)
        {
            Console.WriteLine($"ErrorInUpdatingEmployee: activityId {activityId}, employeeId {employeeId}, exception {exception.GetBaseException().Message}");
        }

        public void ErrorInResolvingRepository(Guid activityId, string dataSourceName, string typeName, Exception exception)
        {
            Console.WriteLine($"ErrorInResolvingRepository: activityId {activityId}, dataSourceName {dataSourceName}, typeName {typeName}, exception {exception.GetBaseException().Message}");
        }
    }
}
