using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


namespace Vin_de_la_france.Models;

[Table("CommandeFournisseursClass")]
[Index("FournisseursClassId", Name = "IX_CommandeFournisseursClass_FournisseursClassId")]
public partial class CommandeFournisseursClass
{
    [Key]
    public int Id { get; set; }

    public DateTimeOffset Date { get; set; }

    public string Statut { get; set; } = null!;

    public int FournisseursClassId { get; set; }

    [ForeignKey("FournisseursClassId")]
    [InverseProperty("CommandeFournisseursClasses")]
    public virtual FournisseursClass FournisseursClass { get; set; } = null!;
}
