using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsistente.AccesoDatos.Modelos
{
    [Table("Asistente")]
    public class AsistenteEF
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int AsistenteID { get; set; }
        public string nombreUsuario { get; set; }
        public int? UnidadID { get; set; }
        public string Accesos { get; set; }
        public int? EncargadoID { get; set; }
        public string Contrasenia { get; set; }
        public int? BadgeID { get; set; }
        public virtual BadgeEF Badge { get; set; }
        public virtual UnidadEF Unidad { get; set; }
        public virtual PersonaEF Persona { get; set; }
        public virtual EncargadoEF Encargado { get; set; }
    }
}
