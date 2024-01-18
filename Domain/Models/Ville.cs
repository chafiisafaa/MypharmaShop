using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Ville
    {
        public Ville()
        {
            Clients = new HashSet<Client>();
            Collaborateurs = new HashSet<Collaborateur>();
        }

        public int Ville_Id { get; set; }
        public string? Ville_Libelle { get; set; }

        public virtual ICollection<Client> Clients { get; set; }
        public virtual ICollection<Collaborateur> Collaborateurs { get; set; }
    }
}
