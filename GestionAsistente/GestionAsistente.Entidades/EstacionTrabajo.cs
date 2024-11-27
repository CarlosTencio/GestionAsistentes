using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsistente.Entidades
{
    public class EstacionTrabajo
    {
        public int EstacionTrabajoID { get; set; }
        public int Numero { get; set; }
        public int Estado { get; set; }
        public int OficinaID { get; set; }

        public Oficina Oficina { get; set; }
    }
}
