using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinWpf.DataSet
{
    public class ArticlesClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UnitPrice { get; set; }
        public int QuantityStock { get; set; }
        public int MinimumThreshold { get; set; }
        public Guid Reference { get; set; }


        [ForeignKey("FamillesClass")]
        public int FamillesClassId { get; set; }
        public virtual FamillesClass FamillesClass { get; set; }

        [ForeignKey("FournisseursClass")]
        public int FournisseursClassId { get; set; }
        public virtual FournisseursClass FournisseursClass { get; set; }
    }
}