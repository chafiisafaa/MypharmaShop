using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning.Models
{
    public class EtataStockModel
    {
        public int? EtatStock_VisiteId { get; set; }
        //3:Rupture,2:Sur Stock
        public int? EtatStock_StatutId { get; set; }
        public int? EtatStock_ProduitId { get; set; }
        public string? EtatStock_Commentaire { get; set; }
    }
}
