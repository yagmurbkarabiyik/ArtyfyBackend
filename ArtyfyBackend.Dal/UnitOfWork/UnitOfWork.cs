using ArtyfyBackend.Core.UnitOfWork;
using ArtyfyBackend.Dal.Context;

namespace ArtyfyBackend.Dal.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ArtyfyBackendDbContext _dbContext;

        public UnitOfWork(ArtyfyBackendDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }
    }
}
