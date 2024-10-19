using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinWpf.DataSet
{
    internal class VinContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Host=localhost;Port=5432;Database=VinDeLaFronce;Username=postgres;Password=Vin";
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            optionsBuilder.UseNpgsql(connectionString);
        }

        public DbSet<ClientsClass> ClientsClass { get; set; }
        public DbSet<FournisseursClass> FournisseursClass { get; set; }
        public DbSet<FamillesClass> FamillesClass { get; set; }
        public DbSet<ArticlesClass> ArticlesClass { get; set; }
        public DbSet<CommandeClientsClass> CommandeClientsClass { get; set; }
        public DbSet<CommandeFournisseursClass> CommandeFournisseursClass { get; set; }
        public DbSet<LigneCommandeFournisseursClass> LigneCommandeFournisseursClass { get; set; }
        public DbSet<LigneCommandeClientsClass> LigneCommandeClientsClass { get; set; }
    }
}
