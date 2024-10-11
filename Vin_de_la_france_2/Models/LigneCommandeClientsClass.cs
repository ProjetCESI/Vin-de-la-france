using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vin_de_la_france_2.Models
{
    [Table("LigneCommandeClientsClass")]
    public class LigneCommandeClientsClass
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Quantite { get; set; }
        public int PrixUnitaire { get; set; }

        public int CommandeClientsClassId { get; set; }
        public virtual CommandeClientsClass CommandeClientsClass { get; set; }

        public int ArticlesClassId { get; set; }
        public virtual ArticlesClass ArticlesClass { get; set; }
    }
}
