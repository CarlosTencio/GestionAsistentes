using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsistente.AccesoDatos.Modelos
{
    [Table("ReporteBadge")]
    public class ReporteBadgeEF
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReporteBadgeID { get; set; }
        public string NombreUsuario { get; set; }
        public int NumeroBadge { get; set; }
        public string NombreAsistente { get; set; }
        public string Accion { get; set; }
        public DateTime Fecha { get; set; }
    }
}
