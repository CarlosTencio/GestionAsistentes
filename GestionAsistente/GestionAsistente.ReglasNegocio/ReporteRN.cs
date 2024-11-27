using GestionAsistente.AccesoDatos.EntidadesAD;
using GestionAsistente.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsistente.ReglasNegocio
{
    public class ReporteRN
    {
        private readonly ReporteBadgeAD reporteAD;

        public ReporteRN()
        {
            reporteAD = new ReporteBadgeAD();
        }

        public async Task<bool> RegistrarReporte(ReporteBadge reporte)
        {
            if (reporte == null)
            {
                throw new ArgumentNullException(nameof(reporte));
            }
            return await reporteAD.RegistrarReporteBadge(reporte);
        }

        public async Task<List<ReporteBadge>> ListarReporte()
        {
            List<ReporteBadge> reportes = new List<ReporteBadge>();
            reportes = await this.reporteAD.listarReporte();
            return reportes;
        }


    }

}