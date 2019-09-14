using System;

namespace Contoso.Phoenix.Data.Entity.Common
{
    public sealed class EntityNameAttribute : Attribute
    {
        public string Value { get; }

        public EntityNameAttribute(string value)
        {
            Value = value;
        }
    }
}
