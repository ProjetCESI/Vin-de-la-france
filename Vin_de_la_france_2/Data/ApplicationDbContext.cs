using Microsoft.EntityFrameworkCore;
using Vin_de_la_france_2.Models;

namespace Vin_de_la_france_2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public DbSet<ArticlesClass> ArticlesClasses { get; set; }
        public DbSet<FamillesClass> FamillesClasses { get; set; }
        public DbSet<FournisseursClass> FournisseursClasses { get; set; }
        public DbSet<ClientsClass> ClientsClass { get; set; }
        public DbSet<CommandeClientsClass> CommandeClientsClasses { get; set; }
        public DbSet<LigneCommandeClientsClass> LigneCommandeClientsClasses { get; set; }
        public DbSet<CommandeFournisseursClass> commandeFournisseursClasses { get; set; }
        public DbSet<LigneCommandeFournisseursClass> LigneCommandeFournisseursClasses { get; set; }
    }
}
