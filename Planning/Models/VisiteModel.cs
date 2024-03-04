using Microsoft.WindowsAzure.Storage.Blob.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning.Models
{
    public class VisiteModel
    {
        public int? Visite_PlanningId { get; set; }
        public List<int>? objetsIds{ get; set; }
        public decimal? Visite_MontantCommande { get; set; }
        public string? Visite_Commentaire { get; set; }
        public int? Visite_NombreVitrine { get; set; }

    }
}
