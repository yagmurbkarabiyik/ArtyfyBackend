using ArtyfyBackend.Core.Models.SubComment;
using ArtyfyBackend.Core.Services;
using ArtyfyBackend.Domain.Entities;

namespace ArtyfyBackend.Core.IServices
{
    public interface ISubCommentService : IGenericService<SubComment, SubCommentModel>
    {
        public Task<List<SubCommentModel>> GetSubCommentsByCommentIdAsync(int commentId);
    }
}