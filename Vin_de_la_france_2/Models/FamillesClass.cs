using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Vin_de_la_france_2.Models;
using System.ComponentModel.DataAnnotations.Schema;  


namespace Vin_de_la_france_2.Models
{
    [Table("FamillesClass")]
    public class FamillesClass
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<ArticlesClass> ArticlesClasses { get; set; }
    }
}
