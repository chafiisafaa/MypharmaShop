using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Visibilite
    {
        public Visibilite()
        {
            VisibilitePhotos = new HashSet<Visibilite_Photo>();
        }

        public int Visibilite_Id { get; set; }
        public int? Visibilite_VisiteId { get; set; }
        public int? Visibilite_NombreVitrine { get; set; }
        public decimal? Visibilite_DimenssionVitrine { get; set; }
        public int? Visibilite_NatureIdVitrine { get; set; }

        public virtual Nature_Vitrine? VisibiliteNatureIdVitrineNavigation { get; set; }
        public virtual Visite? VisibiliteVisite { get; set; }
        public virtual ICollection<Visibilite_Photo> VisibilitePhotos { get; set; }
    }
}
