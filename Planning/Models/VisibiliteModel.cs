using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning.Models
{
    public class VisibiliteModel
    {
        public int? Visibilite_VisiteId { get; set; }
        //public int? Visibilite_NombreVitrine { get; set; }
        public string? Visibilite_DimenssionVitrine { get; set; }
        public int? Visibilite_NatureIdVitrine { get; set; }
        public List<string>? Visibilite_urls { get; set; }
        public bool? Visibilite_Presence { get; set; }
    }
}
