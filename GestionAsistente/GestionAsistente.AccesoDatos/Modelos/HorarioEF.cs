using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsistente.AccesoDatos.Modelos
{
    [Table("Horario")]
    public class HorarioEF
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HorarioID { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime HoraFin { get; set; }
        public string Dia { get; set; }
        public int EstacionTrabajoID { get; set; }
        public int AsistenteID { get; set; }

        public virtual EstacionTrabajoEF EstacionTrabajo { get; set; }
        public virtual AsistenteEF Asistente { get; set; }
    }
}
