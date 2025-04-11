using Microsoft.EntityFrameworkCore;
using Zorgboerderij.Entities;

namespace Zorgboerderij.Entities
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<UserAccount> userAccounts { get; set; }
        public DbSet<Dagcodes> dagCodes { get; set; }
        public DbSet<Bezettingen> bezettingen { get; set; }
        public DbSet<Afmeldingen> afmeldingen { get; set; }
        public DbSet<Personeel> personeel { get; set; }
        public DbSet<Clienten> clienten { get; set; }
        public DbSet<Bakjes> bakjes { get; set; }
        public DbSet<Dagindeling> Dagindelingen { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Zorgboerderij.Entities.Bezettingen> Bezettingen { get; set; } = default!;

    }

}
