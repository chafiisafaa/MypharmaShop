using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Marque
    {
        public Marque()
        {
            Merchandisings = new HashSet<Merchandising>();
            Nouveautes = new HashSet<Nouveaute>();
            Produits = new HashSet<Produit>();
        }

        public int Marque_Id { get; set; }
        public string? Marque_Nom { get; set; }

        public virtual ICollection<Merchandising> Merchandisings { get; set; }
        public virtual ICollection<Nouveaute> Nouveautes { get; set; }
        public virtual ICollection<Produit> Produits { get; set; }
    }
}
