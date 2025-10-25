using Microsoft.EntityFrameworkCore;

namespace OrderMinimalApi.Data
{
    public class WriteDbContext: DbContext
    {
        public WriteDbContext(DbContextOptions<WriteDbContext> options) : base(options)
        {
        }
        public DbSet<Models.Order> Orders => Set<Models.Order>();
    }
}
