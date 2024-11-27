using GestionAsistente.AccesoDatos.Contexto;
using GestionAsistente.AccesoDatos.Modelos;
using GestionAsistente.Entidades;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsistente.AccesoDatos.EntidadesAD
{
    public class UnidadAD
    {
        private readonly GestionAsistenteContexto _contexto;
        // Instancia de HistorialAccionesAD
        private readonly HistorialAccionesAD _historialAccionesAD;
        public UnidadAD() 
        { 
            this._contexto = new GestionAsistenteContexto();
            this._historialAccionesAD = new HistorialAccionesAD();
        }
        public async Task<bool> RegistrarUnidad(Unidad unidad)
        {
            UnidadEF unidadEF = new UnidadEF
            {
                Nombre = unidad.Nombre
            };
            this._contexto.UnidadEFs.Add(unidadEF);
            /**/
            bool resultado = this._contexto.SaveChanges() > 0;
            
            return resultado;

        }
        public async Task<List<Unidad>> listarUnidades()
        {
            List<UnidadEF> unidadEFs = _contexto.UnidadEFs.ToList();
            List<Unidad> unidads = new List<Unidad>();

            foreach (UnidadEF unidadEF in unidadEFs) // Cambiado a UnidadEF
            {
                unidads.Add(new Unidad
                {
                    UnidadID = unidadEF.UnidadID,//añadir el id tambien
                    Nombre = unidadEF.Nombre, // Cambiado a unidadEF
                });
            }

            return unidads;
        }
        public async Task<(string, bool)> ActualizarUnidad(Unidad unidad)
        {
            UnidadEF unidadEF = _contexto.UnidadEFs.Find(unidad.UnidadID);

            if (unidadEF == null)
            {
                return ("La unidad no se pudo actualizar", false);
            }
            unidadEF.Nombre = unidad.Nombre;
            /**/
            bool resultado = this._contexto.SaveChanges() > 0;

            return resultado
                ? ("Actualizado correctamente", true)
                : ("Error al actualizar", false);
        }
        public async Task<(string, bool)> EliminarUnidad(int unidadID)
        {
            UnidadEF unidadEF = this._contexto.UnidadEFs.FirstOrDefault(e => e.UnidadID == unidadID);
            if (unidadEF == null)
            {
               return ("No existe el registro de unidad", false);
            }
            this._contexto.UnidadEFs.Remove(unidadEF);
            /**/
            bool resultado = this._contexto.SaveChanges() > 0;
            
            return resultado
                ? ("Eliminado correctamente", true)
                : ("Error al eliminar", false);

        }
    }
}
