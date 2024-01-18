using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Evenement_Type
    {
        public Evenement_Type()
        {
            SellOuts = new HashSet<SellOut>();
        }

        public int EvenementType_Id { get; set; }
        public string? EvenementType_Libelle { get; set; }

        public virtual ICollection<SellOut> SellOuts { get; set; }
    }
}
