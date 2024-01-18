using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Merchandising
    {
        public Merchandising()
        {
            MerchandisingPhotos = new HashSet<Merchandising_Photo>();
        }

        public int Merchandising_Id { get; set; }
        public int? Merchandising_VisiteId { get; set; }
        public int? Merchandising_TypeId { get; set; }
        public int? Merchandising_MarqueId { get; set; }

        public virtual Marque? MerchandisingMarque { get; set; }
        public virtual Merchandising_Type? MerchandisingType { get; set; }
        public virtual Visite? MerchandisingVisite { get; set; }
        public virtual ICollection<Merchandising_Photo> MerchandisingPhotos { get; set; }
    }
}
