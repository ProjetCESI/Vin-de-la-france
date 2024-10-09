using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Vin_de_la_france.Models;

[Table("LigneCommandeClientsClass")]
[Index("ArticlesClassId", Name = "IX_LigneCommandeClientsClass_ArticlesClassId")]
[Index("CommandeClientsClassId", Name = "IX_LigneCommandeClientsClass_CommandeClientsClassId")]
public partial class LigneCommandeClientsClass
{
    [Key]
    public int Id { get; set; }

    public int Quantite { get; set; }

    public int PrixUnitaire { get; set; }

    public int CommandeClientsClassId { get; set; }

    public int ArticlesClassId { get; set; }

    [ForeignKey("ArticlesClassId")]
    [InverseProperty("LigneCommandeClientsClasses")]
    public virtual ArticlesClass ArticlesClass { get; set; } = null!;

    [ForeignKey("CommandeClientsClassId")]
    [InverseProperty("LigneCommandeClientsClasses")]
    public virtual CommandeClientsClass CommandeClientsClass { get; set; } = null!;
}
