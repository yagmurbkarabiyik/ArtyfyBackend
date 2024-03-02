using ArtyfyBackend.Core.Repositories;
using ArtyfyBackend.Dal.Context;
using ArtyfyBackend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtyfyBackend.Dal.Repositories
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        public CommentRepository(ArtyfyBackendDbContext context) : base(context)
        {
        }
    }
}
