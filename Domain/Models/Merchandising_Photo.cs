using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Merchandising_Photo
    {
        public int MerchandisingPhoto_Id { get; set; }
        public int? MerchandisingPhoto_MerchandisingId { get; set; }
        public string? MerchandisingPhoto_LienFichier { get; set; }

        public virtual Merchandising? MerchandisingPhotoMerchandising { get; set; }
    }
}
