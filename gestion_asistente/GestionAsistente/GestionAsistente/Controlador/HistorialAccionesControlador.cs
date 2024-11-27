using GestionAsistente.AccesoDatos.EntidadesAD;
using GestionAsistente.Entidades;
using GestionAsistente.ReglasNegocio;

namespace GestionAsistente.BlazorUI.Controlador
{
    public class HistorialAccionesControlador
    {
        private readonly HistorialAccionesRN historialAccionesRN;
        public HistorialAccionesControlador()
        {
            this.historialAccionesRN = new HistorialAccionesRN();
        }
        public async Task<bool> RegistrarHistorialAcciones(HistorialAcciones historialAcciones)
        {
            return await historialAccionesRN.RegistrarHistorialAcciones(historialAcciones);
        }
        public List<HistorialAcciones> ListarHistorialPorFecha(DateTime fecha)
        {
            return historialAccionesRN.ListarHistorialPorFecha(fecha);
        }
        
 public List<HistorialAcciones> BuscarAccionPorPersona(string nombrePersona)
        {
            return historialAccionesRN.BuscarAccionPorPersona(nombrePersona);
        }       public List<HistorialAcciones> BuscarPorAccion(string accion)
        {
            return historialAccionesRN.BuscarPorAccion(accion);
        }
        public async Task<List<HistorialAcciones>> ListarHistorial()
        {
            return await historialAccionesRN.ListarHistorial();
        }
        public async Task<Task> LimpiarHistorialCompleto()
        {
            return await historialAccionesRN.LimpiarHistorialCompleto();
        }
        public async Task<Task> BuscarHistorial()
        {
            return await historialAccionesRN.LimpiarHistorialCompleto();
        }
        public List<HistorialAcciones> BuscarHistorial(string persona, string accion, DateTime? fecha)
        {
            return historialAccionesRN.BuscarHistorial(persona, accion, fecha);
        }
        public async Task<List<string>> ListarPersonas()
        {
            return await historialAccionesRN.ListarPersonas();
        }

    }
}
