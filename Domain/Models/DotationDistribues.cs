using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class DotationDistribues
    {
        public int DotationDistribues_Id { get; set; }
        public int? DotationDistribues_VisiteId { get; set; }
        public int? DotationDistribues_ProduitId { get; set; }
        public int? DotationDistribues_Quantites { get; set; }
        public int? DotationDistribues_Chanllenge { get; set; }
        public int? DotationDistribues_Total { get; set; }
        public virtual Visite? DotationDistribuesVisite { get; set; }
    }
}
