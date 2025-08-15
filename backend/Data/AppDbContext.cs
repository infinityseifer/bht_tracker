using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<DrForm> DrForms { get; set; }       // ✅ For Discipline Referral Form
        public DbSet<BhtForm> BhtForms { get; set; }     // ✅ For BHT Form
    }
}
