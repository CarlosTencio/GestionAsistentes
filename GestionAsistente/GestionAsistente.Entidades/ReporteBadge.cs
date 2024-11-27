using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsistente.Entidades
{
    public class ReporteBadge
    {
        public int ReporteBadgeID { get; set; }
        public string NombreUsuario { get; set; }
        public int NumeroBadge { get; set; }
        public string NombreAsistente { get; set; }
        public string Accion { get; set; }
        public DateTime Fecha { get; set; }

    }
}
