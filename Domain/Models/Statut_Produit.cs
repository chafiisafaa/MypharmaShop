using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Statut_Produit
    {
        public Statut_Produit()
        {
            EtatStocks = new HashSet<Etat_Stock>();
        }

        public int StatutProduit_Id { get; set; }
        public string? StatutProduit_Libelle { get; set; }

        public virtual ICollection<Etat_Stock> EtatStocks { get; set; }
    }
}
