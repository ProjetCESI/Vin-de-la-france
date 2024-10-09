using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Vin_de_la_france.Models;

[Table("ClientsClass")]
public partial class ClientsClass
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    [InverseProperty("ClientsClass")]
    public virtual ICollection<CommandeClientsClass> CommandeClientsClasses { get; set; } = new List<CommandeClientsClass>();
}
