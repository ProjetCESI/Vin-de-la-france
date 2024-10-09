using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Vin_de_la_france.Models;  // Ajoutez le namespace contenant les modèles

namespace Vin_de_la_france.Data  // Assurez-vous que ce namespace correspond bien à votre projet
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

            // Relations avec FamillesClass
            modelBuilder.Entity<ArticlesClass>()
                .HasOne(a => a.FamillesClass)
                .WithMany(f => f.ArticlesClasses)
                .HasForeignKey(a => a.FamillesClassId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relations avec FournisseursClass
            modelBuilder.Entity<ArticlesClass>()
                .HasOne(a => a.FournisseursClass)
                .WithMany(f => f.ArticlesClasses)
                .HasForeignKey(a => a.FournisseursClassId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
