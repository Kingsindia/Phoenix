using System;
using System.Threading.Tasks;
using Contoso.Phoenix.Data.Entity.Model.V1;

namespace Contoso.Phoenix.Logic
{
    public class ContentLogic : IContentLogic
    {
        private readonly IRepositoryDataFactory<Content, Guid> _dataFactory;

        public ContentLogic(IRepositoryDataFactory<Content, Guid> dataFactory)
        {
            _dataFactory = dataFactory;
        }

        public async Task Create(Content content)
        {
            var repository = _dataFactory.Get();

            await repository.AddAsync(content);

            await repository.SaveAsync();
        }
    }
}
