using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Statut
    {
        public Statut()
        {
            Clients = new HashSet<Client>();
        }

        public int Statut_Id { get; set; }
        public string? Statut_Libelle { get; set; }

        public virtual ICollection<Client> Clients { get; set; }
    }
}
