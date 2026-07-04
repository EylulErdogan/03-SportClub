using Microsoft.EntityFrameworkCore;
using SporKulubu.Model;

namespace SporKlubuCodeFirstKatmanliMimariProjectUI.Data.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Member> Members { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<Payment> Payments { get; set; }

    }
}