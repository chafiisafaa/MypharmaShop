using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Produit
    {
        public int Produit_Id { get; set; }
        public string? Produit_Designation { get; set; }
        public int? Produit_TypeId { get; set; }
        public int? Produit_MarqueId { get; set; }
        public string? Produit_CodeSH { get; set; }
        public decimal? Produit_PPH { get; set; }
        public string? Produit_LienPhoto { get; set; }

        public virtual Marque? ProduitMarque { get; set; }
        public virtual Type_Produit? ProduitType { get; set; }
    }
}
