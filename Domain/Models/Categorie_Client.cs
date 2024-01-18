using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Categorie_Client
    {
        public Categorie_Client()
        {
            Clients = new HashSet<Client>();
        }

        public int CategorieClient_Id { get; set; }
        public string? CategorieClient_Libelle { get; set; }

        public virtual ICollection<Client> Clients { get; set; }
    }
}
