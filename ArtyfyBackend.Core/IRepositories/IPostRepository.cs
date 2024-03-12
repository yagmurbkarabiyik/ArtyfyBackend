﻿using ArtyfyBackend.Domain.Entities;

namespace ArtyfyBackend.Core.Repositories
{
    public interface IPostRepository : IGenericRepository<Post>
    {
        public Task<List<Post>> Like(int id);
        public Task<List<Post>> GetSellableProductsAsync();
    }
}