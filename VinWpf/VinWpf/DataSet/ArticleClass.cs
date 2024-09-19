using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinWpf.DataSet
{
    public class ArticleClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UnitPrice { get; set; }
        public int QuantityStock { get; set; }
        public int MinimumThreshold { get; set; }

        [ForeignKey("FamilleClass")]
        public int FamilleClassId { get; set; }
        public virtual FamilleClass FamilleClass { get; set; }
    }
}
