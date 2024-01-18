using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Visibilite_Photo
    {
        public int VisibilitePhoto_Id { get; set; }
        public int? VisibilitePhoto_VisibiliteId { get; set; }
        public string? VisibilitePhoto_LienPhoto { get; set; }

        public virtual Visibilite? VisibilitePhotoVisibilite { get; set; }
    }
}
