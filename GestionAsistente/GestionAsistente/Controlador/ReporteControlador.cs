using GestionAsistente.Entidades;
using GestionAsistente.ReglasNegocio;

namespace GestionAsistente.BlazorUI.Controlador
{
    public class ReporteControlador
    {

        private readonly ReporteRN reporteRN;
        public List<ReporteBadge> listaReportes = new List<ReporteBadge>();

        public ReporteControlador()
        {
        reporteRN = new ReporteRN();
        }
        public async Task<bool> RegistrarReporte(ReporteBadge reporteBadge)
        {
            return await reporteRN.RegistrarReporte(reporteBadge);
        }

        public async Task<List<ReporteBadge>> ListarReporte()
        {
            return await reporteRN.ListarReporte();
        }



















    }
}
