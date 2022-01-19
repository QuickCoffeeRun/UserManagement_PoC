using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using UserManagement_PoC.Models;

namespace UserManagement_PoC.Context
{
    public interface IDataContext
    {
        DbSet<User> Users { get; init; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
