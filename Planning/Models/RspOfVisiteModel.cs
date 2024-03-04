using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning.Models
{
    public class RspOfVisiteModel
    {
        public VisiteModel Visite { get; set; }
        public List<EtataStockModel> EtataStockModel { get; set; }
        public List<MerchandisingModel> MerchandisingModel { get; set; }
        public List<VeilleConcurrentielleModel> VeilleConcurrentielleModel { get; set; }
        public SellOutModel SellOutModel { get; set; }
        public NouveautModel NouveautModel { get; set; }
        public List<VisibiliteModel> VisibiliteModel { get; set; }
    }
}
