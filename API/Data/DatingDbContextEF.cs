using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DatingDbContextEF : DbContext
    {
        public DatingDbContextEF(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AppUser> AppUsers { get; set; }
    }
}
