using System.Collections.Generic;

namespace Contoso.Phoenix.Data.Xml
{
    public interface IXmlDataProvider
    {
        ICollection<TEntity> Load<TEntity>();

        bool Save<TEntity>(ICollection<TEntity> entities);
    }
}
