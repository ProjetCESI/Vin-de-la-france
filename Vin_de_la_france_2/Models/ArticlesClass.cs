using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vin_de_la_france_2.Models
{
    [Table("ArticlesClass")]
    public class ArticlesClass
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int UnitPrice { get; set; }
        public int QuantityStock { get; set; }
        public int MinimumThreshold { get; set; }
        public string? Image { get; set; }
        public Guid Reference { get; set; }
        public int FamillesClassId { get; set; }
        public int FournisseursClassId { get; set; }
        public virtual FamillesClass FamillesClass { get; set; }
        public virtual FournisseursClass FournisseursClass { get; set; }
        public virtual ICollection<LigneCommandeClientsClass> LigneCommandeClientsClass { get; set; }
    }
}
