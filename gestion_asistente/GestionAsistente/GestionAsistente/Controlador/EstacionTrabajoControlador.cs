using GestionAsistente.Entidades;
using GestionAsistente.ReglasNegocio;

namespace GestionAsistente.BlazorUI.Controlador
{
    public class EstacionTrabajoControlador
    {

        private readonly EstacionTrabajoRN estacionTrabajoRN;
        public EstacionTrabajoControlador()
        {
            estacionTrabajoRN = new EstacionTrabajoRN();
        }
        public async Task<bool> RegistrarEstacionTrabajo(EstacionTrabajo estacion)
        {
            return await estacionTrabajoRN.RegistrarEstacion(estacion);
        }

        public async Task<int> estacionesPorOficina(int OficinaID)
        {
            return await estacionTrabajoRN.estacionesPorOficina(OficinaID);
        }

        public async Task<bool> EliminarEstacionesPorOficina(int OficinaID)
        {
            return await estacionTrabajoRN.EliminarEstacionPorOficina(OficinaID);
        }
        public async Task<List<EstacionTrabajo>> ListarEstacionPorOficina(int OficinaID)
        {
            return await estacionTrabajoRN.ListarEstacionPorOficina(OficinaID);
        }
    }
}

