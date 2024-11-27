using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsistente.AccesoDatos.Modelos
{
    [Table("Usuario")]
    public class UsuarioEF
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UsuarioID { get; set; }
        public string Contrasenia { get; set; }
        public string NombreUsuario { get; set; }
        public int? RolID { get; set; }
        public int? UnidadID { get; set; }
        public int PersonaID { get; set; }
        public virtual RolEF Rol { get; set; }
        public virtual UnidadEF Unidad { get; set; }
        public virtual PersonaEF Persona { get; set; }

    }
}
