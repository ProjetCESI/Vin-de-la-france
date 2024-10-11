using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Vin_de_la_france_2.Models;

namespace Vin_de_la_france_2.Models
{
    [Table("CommandeFournisseursClass")]
    public class CommandeFournisseursClass
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }
        public string Statut { get; set; }

        public int FournisseursClassId { get; set; }
        public virtual FournisseursClass FournisseursClass { get; set; }
        public virtual ICollection<LigneCommandeFournisseursClass> LigneCommandeFournisseursClass { get; set; }
    }
}
