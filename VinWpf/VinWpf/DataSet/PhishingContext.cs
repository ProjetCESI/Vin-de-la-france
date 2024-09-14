using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinWpf.DataSet
{
    internal class PhishingContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Host=localhost;Port=5432;Database=VinDeLaFronce;Username=postgres;Password=Vin";

            optionsBuilder.UseNpgsql(connectionString);
        }

        public DbSet<ClientsClass> ClientsClass { get; set; }
    }
}
