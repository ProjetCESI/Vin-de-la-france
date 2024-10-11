using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vin_de_la_france_2.Models
{
    [Table("ClientsClass")]
    public class ClientsClass
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public virtual ICollection<CommandeClientsClass> CommandeClientsClasses { get; set; }
    }
}
