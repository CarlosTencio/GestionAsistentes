using GestionAsistente.Entidades;
using GestionAsistente.ReglasNegocio;

namespace GestionAsistente.BlazorUI.Controlador
{
    public class UnidadControlador
    {
        private readonly UnidadRN unidadRN;
        public UnidadControlador()
        {
            this.unidadRN = new UnidadRN();
        }
        public async Task<(bool, string)> RegistrarUnidad(Unidad unidad)
        {
            return await unidadRN.RegistrarUnidad(unidad);
        }
        public async Task<List<Unidad>> ListarUnidades()
        {
            return await unidadRN.ListarUnidades();
        }
        public async Task<(string, bool)> ActualizarUnidad(Unidad unidad)
        {
            return await unidadRN.ActualizarUnidad(unidad);
        }
        public async Task<(string, bool)> EliminarUnidad(int unidadID)
        {
            return await unidadRN.EliminarUnidad(unidadID);
        }
    }
}
