using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GestionAsistente.ReglasNegocio;
using GestionAsistente.Entidades;
using Microsoft.SqlServer.Server;
namespace GestionAsistente.BlazorUI.Controlador
{

    public class AsistenteControlador : Controller
    {
        private readonly AsistenteRN asistenteRN;

        public AsistenteControlador()
        {
            asistenteRN = new AsistenteRN();
        }
        public async Task<(bool, string)> RegistrarAsistente(Asistente asistente)
        {
            return await asistenteRN.RegistrarAsistente(asistente);
        }

        public async Task<List<Asistente>> ListarAsistentes()
        {
            return await asistenteRN.ListarAsistentes();
        }

        public async Task<List<Asistente>> ListarAsistentesPorUnidadID(int? unidad)
        {
            return await asistenteRN.ListarAsistentesPorUnidadID(unidad);
        }

        public async Task<List<Asistente>> BuscarAsistentePorNombre(string nombre)
        {
            return await asistenteRN.BuscarAsistentePorNombre(nombre);
        }

        public async Task<(bool, string)> EliminarAsistente(int? asistenteID)
        {
            return await asistenteRN.EliminarAsistente(asistenteID);
        }

        public async Task<(bool exito, string mensaje)> ActualizarAsistente(Asistente asistente)
        {
            return await asistenteRN.ActualizarAsistente(asistente);
        }

    }
}