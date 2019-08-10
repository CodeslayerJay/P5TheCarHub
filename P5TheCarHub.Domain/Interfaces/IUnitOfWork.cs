using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace P5TheCarHub.Core.Interfaces
{
    public interface IUnitOfWork
    {
        void SaveChanges();
        Task SaveChangesAsync();

    }
}
