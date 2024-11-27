using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsistente.Entidades
{
    public class Badge
    {
        public int BadgeID { get; set; }

        public bool Activo { get; set; }
        public string Justificacion { get; set; }
        public string Horario { get; set; }
        public int? UnidadID { get; set; }
        public Unidad Unidad { get; set; }
        public bool Ocupado { get; set; }
    }
}
