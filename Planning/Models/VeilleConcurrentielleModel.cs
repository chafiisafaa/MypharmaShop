using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning.Models
{
    public class VeilleConcurrentielleModel
    {
        public int? VeilleConcurrentielle_VisiteId { get; set; }
        //1:Rupture ;2:Nouveautés; 3: Actions Spéciales
        public int? VeilleConcurrentielle_TypeId { get; set; }
        public string? VeilleConcurrentielle_Commentaire { get; set; }
        public string? VeilleConcurrentielle_LienPhoto { get; set; }
    }
}
