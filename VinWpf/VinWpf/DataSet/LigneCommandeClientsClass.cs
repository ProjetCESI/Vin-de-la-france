using System.ComponentModel.DataAnnotations.Schema;

namespace VinWpf.DataSet
{
    public class LigneCommandeClientsClass
    {
        public int Id { get; set; }
        public int Quantite { get; set; }
        public int PrixUnitaire { get; set; }

        [ForeignKey("CommandeClientsClass")]
        public int CommandeClientsClassId { get; set; }
        public virtual CommandeClientsClass CommandeClientsClass { get; set; }

        [ForeignKey("ArticlesClass")]
        public int ArticlesClassId { get; set; }
        public virtual ArticlesClass ArticlesClass { get; set; }

        // Propriété calculée pour le prix total
        [NotMapped]
        public int PrixTotal
        {
            get
            {
                return Quantite * PrixUnitaire;
            }
        }
    }
}
