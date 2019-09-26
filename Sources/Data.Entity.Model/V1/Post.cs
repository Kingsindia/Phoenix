using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Contoso.Phoenix.Data.Entity.Common;

namespace Contoso.Phoenix.Data.Entity.Model.V1
{
    [Serializable]
    [DataContract]
    [EntityName(EntityNames.Post)]
    public class Post : IEntity<Guid>
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public Guid UserId { get; set; }

        [DataMember]
        public string PostTitle { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public List<Content> Contents { get; set; }

        [DataMember]
        public List<LikeInfo> Likes { get; set; }
    }
}
