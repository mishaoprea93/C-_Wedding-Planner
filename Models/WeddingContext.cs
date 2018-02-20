using Microsoft.EntityFrameworkCore;
using wedding_planner.Models;

namespace wedding_planner.Models
{
    public class WeddingContext : DbContext
    {
        public WeddingContext(DbContextOptions<WeddingContext> options) : base(options) { }
        public DbSet<wedding_planner.Models.User> users { get; set; }
        public DbSet<Wedding> weddings { get; set; }
        public DbSet<Join> joins{get;set;}
        
        
    }
}
