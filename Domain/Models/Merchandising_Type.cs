using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Merchandising_Type
    {
        public Merchandising_Type()
        {
            Merchandisings = new HashSet<Merchandising>();
        }

        public int MerchandisingType_Id { get; set; }
        public string? MerchandisingType_Libelle { get; set; }

        public virtual ICollection<Merchandising> Merchandisings { get; set; }
    }
}
