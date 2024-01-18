using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Etat_Stock
    {
        public int EtatStock_Id { get; set; }
        public int? EtatStock_VisiteId { get; set; }
        public int? EtatStock_StatutId { get; set; }
        public int? EtatStock_ProduitId { get; set; }
        public string? EtatStock_Commentaire { get; set; }

        public virtual Statut_Produit? EtatStockStatut { get; set; }
        public virtual Visite? EtatStockVisite { get; set; }
    }
}
