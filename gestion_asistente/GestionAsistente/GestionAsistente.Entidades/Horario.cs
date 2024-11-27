using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsistente.Entidades
{
    public class Horario
    {
        public int HorarioID { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime HoraFin { get; set; }
        public string Dia { get; set; }
        public int EstacionTrabajoID { get; set; }
        public int AsistenteID { get; set; }
        public EstacionTrabajo EstacionTrabajo { get; set; }
        public Asistente Asistente { get; set; }
    }
}
