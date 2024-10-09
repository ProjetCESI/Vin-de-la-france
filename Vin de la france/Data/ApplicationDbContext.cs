using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Vin_de_la_france.Models;

namespace Vin_de_la_france.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ArticlesClass> ArticlesClass { get; set; }
        public DbSet<FamillesClass> Familles { get; set; }
        public DbSet<FournisseursClass> Fournisseurs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships and constraints if necessary
            modelBuilder.Entity<ArticlesClass>()
                .HasOne(a => a.FamillesClass)
                .WithMany(f => f.ArticlesClasses)
                .HasForeignKey(a => a.FamillesClassId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ArticlesClass>()
                .HasOne(a => a.FournisseursClass)
                .WithMany(f => f.ArticlesClasses)
                .HasForeignKey(a => a.FournisseursClassId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
