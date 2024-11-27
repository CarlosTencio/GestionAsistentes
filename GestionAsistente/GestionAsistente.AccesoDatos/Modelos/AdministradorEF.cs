using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsistente.AccesoDatos.Modelos
{
    [Table("Administrador")]
    public class AdministradorEF
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AdministradorID { get; set; }
        public string Contrasenia { get; set; }
        public string usuario { get; set; }
        public int PersonaID { get; set; }
        public int RolID { get; set; }
        public int UnidadID { get; set; }
        public virtual RolEF Rol { get; set; }
        public virtual UnidadEF Unidad { get; set; }
        public virtual PersonaEF Persona { get; set; }

    }
}
