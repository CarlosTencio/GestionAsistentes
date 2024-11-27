using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsistente.AccesoDatos.Modelos
{
    [Table("EstacionTrabajo")]
    public class EstacionTrabajoEF
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EstacionTrabajoID { get; set; }
        public int Numero { get; set; }
        public int Estado { get; set; }
        public int OficinaID { get; set; }

        public virtual OficinaEF Oficina { get; set; }
    }
}
