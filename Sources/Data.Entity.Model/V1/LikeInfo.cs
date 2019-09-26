using System;
using System.Runtime.Serialization;
using Contoso.Phoenix.Data.Entity.Common;

namespace Contoso.Phoenix.Data.Entity.Model.V1
{
    [Serializable]
    [DataContract]
    [EntityName(EntityNames.Like)]
    public class LikeInfo : IEntity<Guid>
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public Guid ContentId { get; set; }

        [DataMember]
        public Guid UserId { get; set; }
    }
}
