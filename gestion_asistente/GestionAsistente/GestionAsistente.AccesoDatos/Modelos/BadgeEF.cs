using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsistente.AccesoDatos.Modelos
{
    [Table("Badge")]
    public class BadgeEF
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BadgeID { get; set; }
        
        public bool Activo { get; set; }
        public string Justificacion { get; set; }
        public string Horario { get; set; }
        public int? UnidadID { get; set; }
        public bool Ocupado { get; set; }
        public virtual UnidadEF Unidad { get; set; }
    }
}
