﻿using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Objet_Visite
    {
        public Objet_Visite()
        {
            Visites = new HashSet<Visite>();
        }

        public int ObjetVisite_Id { get; set; }
        public string? ObjetVisite_Libelle { get; set; }

        public virtual ICollection<Visite> Visites { get; set; }
    }
}
