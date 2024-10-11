using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Vin_de_la_france_2.Models;
using System.ComponentModel.DataAnnotations.Schema; 


namespace Vin_de_la_france_2.Models
{
    [Table("FournisseursClass")]
    public class FournisseursClass
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public virtual ICollection<ArticlesClass> ArticlesClasses { get; set; }
        public virtual ICollection<CommandeFournisseursClass> CommandeFournisseursClass { get; set; }

    }
}
