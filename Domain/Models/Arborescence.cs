using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Arborescence
    {
        public int Arborescence_Id { get; set; }
        public int? Arborescence_EntiteId { get; set; }
        public int? Arborescence_TypeId { get; set; }
        public string? Arborescence_Libelle { get; set; }
        public int? Arborescence_ParentId { get; set; }
        public bool? Arborescence_Statut { get; set; }
    }
}
