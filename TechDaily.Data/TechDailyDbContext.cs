using Microsoft.EntityFrameworkCore;
using TechDaily.Domain.Entities;

namespace TechDaily.Infrastructure
{
    public class TechDailyDbContext : DbContext
    {
        public TechDailyDbContext(DbContextOptions options) : base(options)
        {            
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}
