using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsistente.AccesoDatos.Modelos
{
    [Table("Encargado")]
    public class EncargadoEF
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EncargadoID { get; set; }
        public int PersonaID { get; set; }
        public int? UnidadID { get; set; }
        public virtual PersonaEF Persona { get; set; }
        public virtual UnidadEF Unidad { get; set; }

    }
}
