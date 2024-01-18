using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Secteur
    {
        public int Secteur_Id { get; set; }
        public string? Secteur_Libelle { get; set; }
        public int? Secteur_VilleId { get; set; }
    }
}
