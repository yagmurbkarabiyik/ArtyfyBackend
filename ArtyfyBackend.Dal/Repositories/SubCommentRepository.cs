using ArtyfyBackend.Core.IRepositories;
using ArtyfyBackend.Dal.Context;
using ArtyfyBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ArtyfyBackend.Dal.Repositories
{
    public class SubCommentRepository : GenericRepository<SubComment>, ISubCommentRepository
    {
       private readonly ArtyfyBackendDbContext _context;
        public SubCommentRepository(ArtyfyBackendDbContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// This method get sub comments for server.
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        public async Task<List<SubComment>> GetSubCommentsByCommentId(int commentId)
        {
            return await _context.SubComments.AsQueryable()
                .Where(x => x.CommentId == commentId).ToListAsync();
        }
    }
}
