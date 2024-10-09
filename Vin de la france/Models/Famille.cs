using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Vin_de_la_france.Models;


namespace Vin_de_la_france.Models;

public partial class Famille
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    [InverseProperty("FamillesClass")]
    public virtual ICollection<ArticlesClass> ArticlesClasses { get; set; } = new List<ArticlesClass>();
}
