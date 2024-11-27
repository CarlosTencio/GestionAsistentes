using GestionAsistente.Entidades;
using GestionAsistente.ReglasNegocio;

namespace GestionAsistente.BlazorUI.Controlador
{
    public class OficinaControlador
    {

        private readonly OficinaRN oficinaRN;
        public List<Oficina> listaOficinas = new List<Oficina>();

        public OficinaControlador()
        {
            oficinaRN = new OficinaRN();
        }
        public async Task<int> RegistrarOficina(Oficina oficina)
        {
            return await oficinaRN.RegistrarOficina(oficina);
        }

        public async Task<List<Oficina>> ListarOficinas()
        {
            return await oficinaRN.ListarOficinas();
        }

        public async Task<bool> EliminarOficinas(int oficinaID)
        {
            return await oficinaRN.EliminarOficina(oficinaID);
        }

        public async Task<bool> ExisteOficina(string nombre)
        {
            return await oficinaRN.ExisteOficina(nombre);
        }

        public async Task<(string, bool)> ActualizarOficina(Oficina oficina, int cantidadEstaciones)
        {
            return await oficinaRN.ActualizarOficina(oficina, cantidadEstaciones);
        }

    }
}
