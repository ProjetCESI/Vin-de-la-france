using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Vin_de_la_france.Models;

[Table("LigneCommandeFournisseursClass")]
[Index("ArticlesClassId", Name = "IX_LigneCommandeFournisseursClass_ArticlesClassId")]
public partial class LigneCommandeFournisseursClass
{
    [Key]
    public int Id { get; set; }

    public int Quantite { get; set; }

    public int PrixUnitaire { get; set; }

    public int CommandeFournisseursClassId { get; set; }

    public int ArticlesClassId { get; set; }

    [ForeignKey("ArticlesClassId")]
    [InverseProperty("LigneCommandeFournisseursClasses")]
    public virtual ArticlesClass ArticlesClass { get; set; } = null!;
}
