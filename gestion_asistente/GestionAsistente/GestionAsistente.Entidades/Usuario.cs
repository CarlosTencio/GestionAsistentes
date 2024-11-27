using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsistente.Entidades
{
    public class Usuario
    {
        public int UsuarioID { get; set; }
        public string Contrasenia { get; set; }
        public string NombreUsuario { get; set; }
        public int? RolID { get; set; }
        public int? UnidadID { get; set; }
        public int PersonaID { get; set; }
        public virtual Rol Rol { get; set; }
        public virtual Unidad Unidad { get; set; }
        public virtual Persona Persona { get; set; }
    }
}
