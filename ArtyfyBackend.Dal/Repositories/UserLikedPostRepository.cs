using ArtyfyBackend.Core.IRepositories;
using ArtyfyBackend.Dal.Context;
using ArtyfyBackend.Domain.Entities;

namespace ArtyfyBackend.Dal.Repositories
{
	public class UserLikedPostRepository : GenericRepository<UserLikedPost>, IUserLikedPostRepository
	{
		public UserLikedPostRepository(ArtyfyBackendDbContext context) : base(context)
		{
		}
	}
}