using GestionEvenement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GestionEvenement.Domain.Contexts
{
    public class ApplicationDbContext : DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Evenement> Evenements { get; set; }
    }
}
