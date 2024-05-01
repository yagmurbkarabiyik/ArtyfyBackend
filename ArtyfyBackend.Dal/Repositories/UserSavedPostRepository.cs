using ArtyfyBackend.Core.IRepositories;
using ArtyfyBackend.Dal.Context;
using ArtyfyBackend.Domain.Entities;

namespace ArtyfyBackend.Dal.Repositories
{
	public class UserSavedPostRepository : GenericRepository<UserSavedPost>, IUserSavedPostRepository
	{
		public UserSavedPostRepository(ArtyfyBackendDbContext context) : base(context)
		{
		}
	}
}