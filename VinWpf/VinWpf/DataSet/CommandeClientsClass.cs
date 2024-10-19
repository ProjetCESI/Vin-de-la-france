using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinWpf.DataSet
{
    public class CommandeClientsClass
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Statut { get; set; }

        [ForeignKey("ClientsClass")]
        public int ClientsClassId { get; set; }
        public virtual ClientsClass ClientsClass { get; set; }

        public virtual ICollection<LigneCommandeClientsClass> LigneCommandes { get; set; }

        public decimal PrixTotal => LigneCommandes?.Sum(l => l.PrixUnitaire * l.Quantite) ?? 0;
    }
}
