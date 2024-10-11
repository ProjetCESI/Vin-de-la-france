using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Vin_de_la_france_2.Models;

namespace Vin_de_la_france_2.Models
{
    [Table("CommandeClientsClass")]
    public class CommandeClientsClass
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }
        public string Statut { get; set; }

        public int ClientsClassId { get; set; }
        public virtual ClientsClass ClientsClass { get; set; }
        public virtual ICollection<LigneCommandeClientsClass> LigneCommandeClientsClass { get; set; }
    }
}
