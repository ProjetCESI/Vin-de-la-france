using System.ComponentModel.DataAnnotations;

namespace Vin_de_la_france_2.Models
{
    public class ClientsClass
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom est obligatoire.")]
        [StringLength(50, ErrorMessage = "Le nom ne peut pas dépasser 50 caractères.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "L'adresse est obligatoire.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "L'email est obligatoire.")]
        [EmailAddress(ErrorMessage = "L'email doit être valide.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Le mot de passe est obligatoire.")]
        [MinLength(6, ErrorMessage = "Le mot de passe doit comporter au moins 6 caractères.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Le numéro de téléphone est obligatoire.")]
        [Phone(ErrorMessage = "Le numéro de téléphone doit être valide.")]
        public string Phone { get; set; }
    }
}
