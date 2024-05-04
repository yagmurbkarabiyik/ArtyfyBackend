using ArtyfyBackend.Core.IRepositories;
using ArtyfyBackend.Dal.Context;
using ArtyfyBackend.Domain.Entities;

namespace ArtyfyBackend.Dal.Repositories
{
    public class UserPostImageRepository : GenericRepository<UserPostImage>, IUserPostImageRepository
    {
        public UserPostImageRepository(ArtyfyBackendDbContext context) : base(context)
        {
        }
    }
}