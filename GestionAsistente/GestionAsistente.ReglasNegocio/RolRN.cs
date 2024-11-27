using GestionAsistente.AccesoDatos.EntidadesAD;
using GestionAsistente.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsistente.ReglasNegocio
{
    public class RolRN
    {
        
        private readonly RolAD rolAD;
        public RolRN()
        {
            this.rolAD = new RolAD();
        }

        public async Task<List<Rol>> ListarRoles()
        {
            List<Rol> roles = new List<Rol>();
            roles = await this.rolAD.listarRoles();
            return roles;
        }

    }
}
