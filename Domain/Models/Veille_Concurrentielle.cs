using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Veille_Concurrentielle
    {
        public int VeilleConcurrentielle_Id { get; set; }
        public int? VeilleConcurrentielle_VisiteId { get; set; }
        public int? VeilleConcurrentielle_TypeId { get; set; }
        public string? VeilleConcurrentielle_Commentaire { get; set; }
        public string? VeilleConcurrentielle_LienPhoto { get; set; }

        public virtual Visite? VeilleConcurrentielleVisite { get; set; }
    }
}
