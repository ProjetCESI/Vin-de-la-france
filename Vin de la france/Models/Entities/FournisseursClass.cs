using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Vin_de_la_france.Models.Entities
{
    public class FournisseursClass
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;

        [Required]
        public string Phone { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        // Liste des vins associés à ce fournisseur
        public ICollection<Vin> Vins { get; set; } = new List<Vin>();
    }
}
