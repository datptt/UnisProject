using System;
using System.Threading.Tasks;

namespace Unis.Repository
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
    }
}
