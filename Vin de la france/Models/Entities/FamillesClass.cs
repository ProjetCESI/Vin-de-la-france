using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Vin_de_la_france.Models.Entities
{
    public class FamillesClass
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        // Liste des vins associés à cette famille
        public ICollection<Vin> Vins { get; set; } = new List<Vin>();
    }
}
