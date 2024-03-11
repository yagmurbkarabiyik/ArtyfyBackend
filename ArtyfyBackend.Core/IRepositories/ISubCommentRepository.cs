using ArtyfyBackend.Core.Repositories;
using ArtyfyBackend.Domain.Entities;

namespace ArtyfyBackend.Core.IRepositories
{
    public interface ISubCommentRepository : IGenericRepository<SubComment>
    {
        public Task<List<SubComment>> GetSubCommentsByCommentId(int commentId);
    }
}