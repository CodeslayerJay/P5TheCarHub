using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace P5TheCarHub.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
