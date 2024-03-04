using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning.Models
{
    public class MerchandisingModel
    {
        public int? Merchandising_VisiteId { get; set; }
        //1:Descente; 2:Réglette; 3:Fronton
        public int? Merchandising_TypeId { get; set; }
        public int? Merchandising_MarqueId { get; set; }

    }
}
