using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class PrdVentesParMarque
    {
        public int PrdVentesParMarque_Id { get; set; }
        public int? PrdVentesParMarque_VisiteId { get; set; }
        public int? PrdVentesParMarque_ProduitId { get; set; }
        public int? PrdVentesParMarque_Nombre { get; set; }
        public int? PrdVentesParMarque_MarqueId { get; set; }
        public virtual Visite? Visite { get; set; }
        
    }
}
