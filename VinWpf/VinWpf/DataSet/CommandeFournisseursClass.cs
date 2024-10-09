using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static VinWpf.Views.ListeCommandeFournisseurs;

namespace VinWpf.DataSet
{
    public class CommandeFournisseursClass
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Statut { get; set; }

        [ForeignKey("FournisseursClass")]
        public int FournisseursClassId { get; set; }
        public virtual FournisseursClass FournisseursClass { get; set; }
    }
}
