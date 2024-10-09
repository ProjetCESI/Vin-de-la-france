using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Vin_de_la_france.Models;

namespace Vin_de_la_france.Models
{
    public class FamillesClass
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        // Liste des vins associés à cette famille
        public ICollection<ArticlesClass> ArticlesClasses { get; set; } = new List<ArticlesClass>();
    }
}
