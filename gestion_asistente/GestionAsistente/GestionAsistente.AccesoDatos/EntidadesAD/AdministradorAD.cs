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
    public class AdministradorAD
    {
        private readonly GestionAsistenteContexto _contexto;
        public AdministradorAD()
        {
            this._contexto = new GestionAsistenteContexto();
        }
        public bool RegistrarAdministrador(Administrador administrador)
        {
            AdministradorEF administradorEF = new AdministradorEF
            {
                Contrasenia = administrador.Contrasenia,
                usuario = administrador.usuario,
                PersonaID = administrador.PersonaID,
                RolID = administrador.RolID,
                UnidadID = administrador.UnidadID
            };
            this._contexto.AdministradorEFs.Add(administradorEF);
            return this._contexto.SaveChanges() > 0;
        }
        public bool VerificarAdministrador(Administrador administrador)
        {
            AdministradorEF administradorEF = _contexto.AdministradorEFs.FirstOrDefault(a => a.usuario == administrador.usuario && a.Contrasenia == administrador.Contrasenia);
            return administradorEF != null;
        }
    }
}
