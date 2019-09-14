using System;
using System.Runtime.Serialization;

namespace Contoso.Phoenix.Data.Entity.Common.Exceptions
{
    [Serializable]
    public class EntityConflictException : Exception
    {
        private const string EntityNameSerializationKey = "entityName";

        public string EntityName { get; private set; }

        public EntityConflictException(string entityName)
        {
            EntityName = entityName;
        }

        public EntityConflictException(string entityName, string message)
            : base(message)
        {
            EntityName = entityName;
        }

        public EntityConflictException(string entityName, string message, Exception inner)
            : base(message, inner)
        {
            EntityName = entityName;
        }

        protected EntityConflictException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            EntityName = info.GetString(EntityNameSerializationKey);
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue(EntityNameSerializationKey, EntityName);
        }
    }
}
