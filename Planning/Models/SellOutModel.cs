using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning.Models
{
    public class SellOutModel
    {
        public int? SellOut_VisiteId { get; set; }
        public bool? SellOut_Animation { get; set; }
        public int? SellOut_NombreAnimationMois { get; set; }
        public int? SellOut_NombreAnimationJour { get; set; }
        public int? SellOut_EvenementTypeId { get; set; }
    }
}
