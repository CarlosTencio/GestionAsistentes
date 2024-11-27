using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsistente.AccesoDatos.Modelos
{
    [Table("HistorialAcciones")]
    public class HistorialAccionesEF
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HistoriaAccionesID { get; set; }
        public DateTime Fecha { get; set; }
        public string NombrePersona { get; set; }
        public string Accion { get; set; }
    }
}
