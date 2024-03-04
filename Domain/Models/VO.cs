using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class VO
    {
        public int VO_Id { get; set; }
        public int? VO_VisiteId { get; set; }
        public int? VO_ObjetVisiteId { get; set; }
        public Visite? visite { get; set; }
        public Objet_Visite? objetVisite { get; set; }
    }
}
