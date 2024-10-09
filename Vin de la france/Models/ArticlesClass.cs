using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Vin_de_la_france.Models.Entities;

namespace Vin_de_la_france.Models;

[Table("ArticlesClass")]
[Index("FamillesClassId", Name = "IX_ArticlesClass_FamillesClassId")]
[Index("FournisseursClassId", Name = "IX_ArticlesClass_FournisseursClassId")]
public partial class ArticlesClass
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int UnitPrice { get; set; }

    public int QuantityStock { get; set; }

    public int MinimumThreshold { get; set; }

    public Guid Reference { get; set; }

    public int FamillesClassId { get; set; }

    public int FournisseursClassId { get; set; }

    [ForeignKey("FamillesClassId")]
    [InverseProperty("ArticlesClasses")]
    public virtual FamillesClass FamillesClass { get; set; } = null!;

    [ForeignKey("FournisseursClassId")]
    [InverseProperty("ArticlesClasses")]
    public virtual FournisseursClass FournisseursClass { get; set; } = null!;

    [InverseProperty("ArticlesClass")]
    public virtual ICollection<LigneCommandeClientsClass> LigneCommandeClientsClasses { get; set; } = new List<LigneCommandeClientsClass>();

    [InverseProperty("ArticlesClass")]
    public virtual ICollection<LigneCommandeFournisseursClass> LigneCommandeFournisseursClasses { get; set; } = new List<LigneCommandeFournisseursClass>();
}
