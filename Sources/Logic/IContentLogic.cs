using System.Threading.Tasks;
using Contoso.Phoenix.Data.Entity.Model.V1;

namespace Contoso.Phoenix.Logic
{
    public interface IContentLogic
    {
        Task Create(Content content);
    }
}