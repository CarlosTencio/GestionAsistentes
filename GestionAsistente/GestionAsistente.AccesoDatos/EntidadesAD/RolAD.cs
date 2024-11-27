using GestionAsistente.AccesoDatos.Contexto;
using GestionAsistente.AccesoDatos.Modelos;
using GestionAsistente.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsistente.AccesoDatos.EntidadesAD
{
    public class RolAD
    {
        private readonly GestionAsistenteContexto _contexto;
        public RolAD()
        {
            this._contexto = new GestionAsistenteContexto();
        }
        public bool RegistrarRol(Rol rol)
        {
            RolEF rolEF = new RolEF
            {
                Nombre = rol.Nombre
            };
            this._contexto.RolEFs.Add(rolEF);
            return this._contexto.SaveChanges() > 0;
        }
        public async Task<List<Rol>> listarRoles()
        {
            List<RolEF> rolEFs = _contexto.RolEFs.ToList();
            List<Rol> roles = new List<Rol>();

            foreach (RolEF rolEF in rolEFs)
            {
                roles.Add(new Rol
                {
                    RolID = rolEF.RolID,
                    Nombre = rolEF.Nombre
                });
            }

            return roles;
        }
        public bool ModificarRol(Rol rol)
        {
            RolEF rolEF = _contexto.RolEFs.Find(rol.RolID);

            if (rolEF == null)
            {
                return false;
            }
            rolEF.Nombre = rol.Nombre;
            return _contexto.SaveChanges() > 0;
        }
        public bool EliminarRol(int RolID)
        {
            RolEF rolEF = _contexto.RolEFs.Find(RolID);

            if (rolEF == null)
            {
                return false;
            }
            _contexto.RolEFs.Remove(rolEF);
            return _contexto.SaveChanges() > 0;
        }
        public Rol BuscarRol(int RolID)
        {
            RolEF rolEF = _contexto.RolEFs.Find(RolID);
            if (rolEF == null)
            {
                return null;
            }
            return new Rol
            {
                RolID = rolEF.RolID,
                Nombre = rolEF.Nombre
            };
        }

    }
}
