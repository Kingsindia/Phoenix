using System.Collections.Generic;
using System.Threading.Tasks;
using Contoso.Phoenix.Data.Entity.Model.V1;

namespace Contoso.Phoenix.Logic
{
    public interface IPostLogic
    {
        Task Create(Post post);

        Task<IList<Post>> GetAllAsync();
    }
}