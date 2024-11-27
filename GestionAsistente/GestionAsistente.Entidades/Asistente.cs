using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsistente.Entidades
{
    public class Asistente
    {
        public int AsistenteID { get; set; }
        public string nombreUsuario { get; set; }
        public int? UnidadID { get; set; }
        public string Accesos { get; set; }
        public int? EncargadoID { get; set; }
        public string Contrasenia { get; set; }
        public int? BadgeID { get; set; }
        public Badge Badge { get; set; }
        public Unidad Unidad { get; set; }
        public Persona Persona { get; set; }
        public Encargado Encargado { get; set; }
    }
}
