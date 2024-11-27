using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsistente.Entidades
{
    public class HistorialAcciones
    {
        public int HistoriaAccionesID { get; set; }
        public DateTime Fecha { get; set; }
        public string NombrePersona { get; set; }
        public string Accion { get; set; }
        
    }
}
