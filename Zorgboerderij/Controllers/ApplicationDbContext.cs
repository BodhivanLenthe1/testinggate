using Microsoft.EntityFrameworkCore;
using Zorgboerderij.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Aanpasbaarheid> Aanpasbaarheid { get; set; }

}
