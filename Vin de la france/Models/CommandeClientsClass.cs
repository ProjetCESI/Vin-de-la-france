using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Vin_de_la_france.Models;

[Table("CommandeClientsClass")]
[Index("ClientsClassId", Name = "IX_CommandeClientsClass_ClientsClassId")]
public partial class CommandeClientsClass
{
    [Key]
    public int Id { get; set; }

    public DateTimeOffset Date { get; set; }

    public string Statut { get; set; } = null!;

    public int ClientsClassId { get; set; }

    [ForeignKey("ClientsClassId")]
    [InverseProperty("CommandeClientsClasses")]
    public virtual ClientsClass ClientsClass { get; set; } = null!;

    [InverseProperty("CommandeClientsClass")]
    public virtual ICollection<LigneCommandeClientsClass> LigneCommandeClientsClasses { get; set; } = new List<LigneCommandeClientsClass>();
}
