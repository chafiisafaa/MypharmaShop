using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Visite
    {
        public Visite()
        {
            EtatStocks = new HashSet<Etat_Stock>();
            Merchandisings = new HashSet<Merchandising>();
            Nouveautes = new HashSet<Nouveaute>();
            SellOuts = new HashSet<SellOut>();
            VeilleConcurrentielles = new HashSet<Veille_Concurrentielle>();
            Visibilites = new HashSet<Visibilite>();
        }

        public int Visite_Id { get; set; }
        public int? Visite_PlanningId { get; set; }
        public DateTime? Visite_Date { get; set; }
        public TimeSpan? Visite_Heure { get; set; }
       // public int? Visite_ObjetId { get; set; }
        public bool? Visite_AvecAnimation { get; set; }
        public int? Visite_TypeAnimationId { get; set; }
        public bool? Visite_MecanismeIinventive { get; set; }
        public bool? Visite_EvenementSpeciale { get; set; }
        public string? Visite_EvenementSpecialeNature { get; set; }
        public string? Visite_Commentaire { get; set; }
        public decimal? Visite_MontantCommande { get; set; }
        public int? Visite_NombreVitrine { get; set; }

        //public virtual Objet_Visite? VisiteObjet { get; set; }
        public virtual PlanningVisite? VisitePlanning { get; set; }
        public virtual Type_Animation? VisiteTypeAnimation { get; set; }
        public virtual ICollection<Etat_Stock> EtatStocks { get; set; }
        public virtual ICollection<Merchandising> Merchandisings { get; set; }
        public virtual ICollection<Nouveaute> Nouveautes { get; set; }
        public virtual ICollection<SellOut> SellOuts { get; set; }
        public virtual ICollection<Veille_Concurrentielle> VeilleConcurrentielles { get; set; }
        public virtual ICollection<Visibilite> Visibilites { get; set; }
        public virtual ICollection<VO> Vos { get; set; }
    }
}
