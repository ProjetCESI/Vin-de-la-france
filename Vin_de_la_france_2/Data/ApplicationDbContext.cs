using Microsoft.EntityFrameworkCore;
using Vin_de_la_france_2.Models;

namespace Vin_de_la_france_2.Data // Assurez-vous que l'espace de noms est correct
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DbSets
        public DbSet<ArticlesClass> ArticlesClasses { get; set; }
        public DbSet<FamillesClass> FamillesClasses { get; set; }
        public DbSet<FournisseursClass> FournisseursClasses { get; set; }
    }
}
