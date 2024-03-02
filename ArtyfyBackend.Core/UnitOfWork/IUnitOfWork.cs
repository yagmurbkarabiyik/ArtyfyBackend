using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtyfyBackend.Core.UnitOfWork
{
    public interface IUnitOfWork
    { 
        //For async operations
        Task CommitAsync();

        //For sync operations
        void Commit();
    }
}
