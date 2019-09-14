using System;
using System.Runtime.Serialization;
using Contoso.Phoenix.Data.Entity.Common;

namespace Contoso.Phoenix.Data.Entity.Model.V1
{
    [Serializable]
    [DataContract]
    [EntityName(EntityNames.Employee)]
    public class Employee : IEntity<int>, IExtensibleDataObject
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Designation { get; set; }

        [DataMember]
        public int? Age { get; set; }

        [DataMember]
        public Address Address { get; set; }

        public ExtensionDataObject ExtensionData { get; set; }
    }
}
