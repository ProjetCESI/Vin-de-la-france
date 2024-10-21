using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinWpf.DataSet
{
    public class ClientsClass
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom est requis.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "L'adresse est requise.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "L'email est requis.")]
        [EmailAddress(ErrorMessage = "L'email n'est pas valide.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Le mot de passe est requis.")]
        [MinLength(6, ErrorMessage = "Le mot de passe doit contenir au moins 6 caractères.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Le téléphone est requis.")]
        public string Phone { get; set; }

        public virtual ICollection<CommandeClientsClass> CommandeClientsClasses { get; set; }
    }
}
