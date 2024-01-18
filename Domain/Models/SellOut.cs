using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class SellOut
    {
        public int SellOut_Id { get; set; }
        public int? SellOut_VisiteId { get; set; }
        public bool? SellOut_Animation { get; set; }
        public int? SellOut_NombreAnimationMois { get; set; }
        public int? SellOut_NombreAnimationJour { get; set; }
        public int? SellOut_EvenementTypeId { get; set; }
        public string? SellOut_AutreTypeEvenement { get; set; }

        public virtual Evenement_Type? SellOutEvenementType { get; set; }
        public virtual Visite? SellOutVisite { get; set; }
    }
}
