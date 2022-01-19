using Microsoft.EntityFrameworkCore;
using UserManagement_PoC.Models;

namespace UserManagement_PoC.Context
{
    public class DataContext : DbContext, IDataContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; init; }
    }
}
