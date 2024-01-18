using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Client
    {
        public Client()
        {
            planningVisites = new HashSet<PlanningVisite>();
        }

        public int Client_Id { get; set; }
        public string? Client_RaisonSociale { get; set; }
        public string? Client_NomCommercial { get; set; }
        public string? Client_CLN { get; set; }
        public string? Client_Adresse { get; set; }
        public int? Client_VilleId { get; set; }
        public string? Client_Tel { get; set; }
        public string? Client_Contact { get; set; }
        public string? Client_RegionPharmashop { get; set; }
        public bool? Client_SiteEcommerce { get; set; }
        public bool? Client_ReseauxSociaux { get; set; }
        public string? Client_Email { get; set; }
        public bool? Client_Contentieux { get; set; }
        public int? Client_StatutId { get; set; }
        public int? Client_ConditionPaiement { get; set; }
        public string? Client_RC { get; set; }
        public string? Client_Patente { get; set; }
        public string? Client_IF { get; set; }
        public string? Client_CINGerant { get; set; }
        public string? Client_ICE { get; set; }
        public int? Client_CatégorieId { get; set; }
        public int? Client_Potentiel { get; set; }
        public string? Client_Representant { get; set; }

        public Categorie_Client? ClientCatégorie { get; set; }
        public Statut? ClientStatut { get; set; }
        public Ville? ClientVille { get; set; }
        public ICollection<PlanningVisite> planningVisites { get; set; }
    }
}
