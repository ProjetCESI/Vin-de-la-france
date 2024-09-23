using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinWpf.DataSet
{
    public class CommandeFournisseurClass
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Quantite { get; set; }

        [ForeignKey("ArticleClass")]
        public int ArticleClassId { get; set; }
        public virtual ArticleClass ArticleClass { get; set; }
    }
}
