using System;

namespace Contoso.Phoenix.Data.Xml.Instrumentation
{
    public interface IXmlDataProviderLogger
    {
        void SerializationError(Guid activityId, string typeName, Exception exception);

        void DeserializationError(Guid activityId, string typeName, Exception exception);
    }
}
