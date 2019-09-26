using System;
using Contoso.Phoenix.Data.Entity.Common;

namespace Contoso.Phoenix.Data.Entity.Model.V1
{
    public class User : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }
    }
}
