using GestionAsistente.AccesoDatos.EntidadesAD;
using GestionAsistente.AccesoDatos.Modelos;
using GestionAsistente.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsistente.ReglasNegocio
{
    public class HistorialAccionesRN
    {
        private readonly HistorialAccionesAD historialAccionesAD;
        public HistorialAccionesRN()
        {
            this.historialAccionesAD = new HistorialAccionesAD();
        }
        public async Task<bool> RegistrarHistorialAcciones(HistorialAcciones historialAcciones)
        {
            if (historialAcciones == null || string.IsNullOrWhiteSpace(historialAcciones.NombrePersona) || string.IsNullOrWhiteSpace(historialAcciones.Accion))
            {
               return false;
            }
            /**/
            //historialAcciones.Accion = $"{historialAcciones.Accion} en la unidad {historialAcciones.NombreUnidad}"; // Concatenación
            return await historialAccionesAD.RegistrarHistorialAcciones(historialAcciones);
        }
        public List<HistorialAcciones> ListarHistorialPorFecha(DateTime fecha)
        {
            return historialAccionesAD.listarHistorialAcciones(fecha);
        }
        public List<HistorialAcciones> BuscarAccionPorPersona(string nombrePersona)
        {
            if (string.IsNullOrWhiteSpace(nombrePersona))
            {
                throw new ArgumentException("El nombre de la persona no puede estar vacío.");
            }

            return historialAccionesAD.BuscarAccionPorPersona(nombrePersona);
        }
        public List<HistorialAcciones> BuscarPorAccion(string accion)
        {
            if (string.IsNullOrWhiteSpace(accion))
            {
                throw new ArgumentException("La acción no puede estar vacía.");
            }

            return historialAccionesAD.BuscarPorAccion(accion);
        }
        /*Nuevo*/
        public async Task<List<HistorialAcciones>> ListarHistorial()
        {
            List<HistorialAcciones> historialAcciones = new List<HistorialAcciones>();
            historialAcciones = await this.historialAccionesAD.ListarHistorial();
            return historialAcciones;
        }
        public async Task<Task> LimpiarHistorialCompleto()
        {
            return historialAccionesAD.LimpiarHistorialCompleto();
        }
        public List<HistorialAcciones> BuscarHistorial(string persona, string accion, DateTime? fecha)
        {
            return historialAccionesAD.BuscarHistorial(persona, accion, fecha);
        }
        public async Task<List<string>> ListarPersonas()
        {
            return await historialAccionesAD.ListarPersonas();
        }

    }
}
