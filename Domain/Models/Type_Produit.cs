using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Type_Produit
    {
        public Type_Produit()
        {
            Produits = new HashSet<Produit>();
        }

        public int TypeProduit_Id { get; set; }
        public string? TypeProduit_Libelle { get; set; }

        public virtual ICollection<Produit> Produits { get; set; }
    }
}
