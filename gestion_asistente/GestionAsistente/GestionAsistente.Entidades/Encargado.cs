using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsistente.Entidades
{
    public class Encargado
    {
        public int EncargadoID { get; set; }
        public int PersonaID { get; set; }
        public int? UnidadID { get; set; }
        public Persona Persona { get; set; }
        public Unidad Unidad { get; set; }
    }
}
