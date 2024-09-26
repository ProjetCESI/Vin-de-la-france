using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinWpf.DataSet
{
    public class LigneCommandeFournisseursClass
    {
        public int Id { get; set; }
        public int Quantite { get; set; }
        public int PrixUnitaire { get; set; }

        [ForeignKey("CommandeFournisseursClass")]
        public int CommandeFournisseursClassId { get; set; }
        public virtual CommandeFournisseursClass CommandeFournisseursClass { get; set; }

        [ForeignKey("ArticlesClass")]
        public int ArticlesClassId { get; set; }
        public virtual ArticlesClass ArticlesClass { get; set; }
    }
}
