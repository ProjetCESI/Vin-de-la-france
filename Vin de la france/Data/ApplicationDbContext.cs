using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Vin_de_la_france.Models.Entities;

namespace Vin_de_la_france.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Vin> Vins { get; set; }
        public DbSet<FamillesClass> Familles { get; set; }
        public DbSet<FournisseursClass> Fournisseurs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuration des relations et contraintes
            modelBuilder.Entity<Vin>()
                .HasOne(v => v.Famille)
                .WithMany(f => f.Vins)
                .HasForeignKey(v => v.FamillesClassId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Vin>()
                .HasOne(v => v.Fournisseur)
                .WithMany(f => f.Vins)
                .HasForeignKey(v => v.FournisseursClassId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
