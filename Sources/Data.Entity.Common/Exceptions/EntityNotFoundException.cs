using System;
using System.Runtime.Serialization;

namespace Contoso.Phoenix.Data.Entity.Common.Exceptions
{
    [Serializable]
    public class EntityNotFoundException : Exception
    {
        private const string EntityNameSerializationKey = "entityName";

        public string EntityName { get; private set; }

        public EntityNotFoundException(string entityName)
        {
            EntityName = entityName;
        }

        public EntityNotFoundException(string entityName, string message)
            : base(message)
        {
            EntityName = entityName;
        }

        public EntityNotFoundException(string entityName, string message, Exception inner)
            : base(message, inner)
        {
            EntityName = entityName;
        }

        protected EntityNotFoundException(SerializationInfo info, StreamingContext context)
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
