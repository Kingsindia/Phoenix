using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contoso.Phoenix.Data.Entity.Model.V1;

namespace Contoso.Phoenix.Logic
{
    public class PostLogic : IPostLogic
    {
        private readonly IRepositoryDataFactory<Post, Guid> _dataFactory;

        public PostLogic(IRepositoryDataFactory<Post, Guid> dataFactory)
        {
            _dataFactory = dataFactory;
        }

        public async Task<IList<Post>> GetAllAsync()
        {
            var repository = _dataFactory.Get();

            var posts = await repository.GetAllAsync();

            return posts?.ToList();
        }

        public async Task Create(Post post)
        {
            var repository = _dataFactory.Get();

            await repository.AddAsync(post);

            await repository.SaveAsync();
        }
    }
}
