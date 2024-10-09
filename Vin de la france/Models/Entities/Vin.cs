using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vin_de_la_france.Models.Entities
{
    public class Vin
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int UnitPrice { get; set; }

        [Required]
        public int QuantityStock { get; set; }

        public int FamillesClassId { get; set; }
        public int FournisseursClassId { get; set; }

        // Navigation properties
        public FamillesClass? Famille { get; set; }
        public FournisseursClass? Fournisseur { get; set; }
    }
}
