using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Contoso.Phoenix.Data.Entity.Common;

namespace Contoso.Phoenix.Data.Entity.Model.V1
{
    [Serializable]
    [DataContract]
    [EntityName(EntityNames.Content)]
    public class Content : IEntity<Guid>
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public ContentTypeEnum ContentType { get; set; }

        [DataMember]
        public List<LikeInfo> Likes { get; set; }
    }
}
