using GestionAsistente.AccesoDatos.Contexto;
using GestionAsistente.AccesoDatos.Modelos;
using GestionAsistente.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsistente.AccesoDatos.EntidadesAD
{
    public class ReporteBadgeAD
    {
        private readonly GestionAsistenteContexto _contexto;
        public ReporteBadgeAD()
        {
            this._contexto = new GestionAsistenteContexto();
        }
        public async Task<bool> RegistrarReporteBadge(ReporteBadge reporteBadge)
        {

            ReporteBadgeEF reporteBadgeEF = new ReporteBadgeEF
            {
                Accion = reporteBadge.Accion,
                NombreUsuario = reporteBadge.NombreUsuario,
                NumeroBadge = reporteBadge.NumeroBadge,
                NombreAsistente = reporteBadge.NombreAsistente,
                Fecha = DateTime.Now
            };
            this._contexto.ReporteBadgeEFs.Add(reporteBadgeEF);
            return this._contexto.SaveChanges() > 0;
        }

        public async Task<List<ReporteBadge>> listarReporte()
        {
            List<ReporteBadgeEF> reporteEFs = _contexto.ReporteBadgeEFs.ToList();
            List<ReporteBadge> reportes = new List<ReporteBadge>();

            foreach (ReporteBadgeEF reporteEF in reporteEFs) 
            {
                reportes.Add(new ReporteBadge
                {
                    Accion = reporteEF.Accion,
                    NombreUsuario = reporteEF.NombreUsuario,
                    NumeroBadge = reporteEF.NumeroBadge,
                    NombreAsistente = reporteEF.NombreAsistente, 
                    Fecha = reporteEF.Fecha
                });
            }

            return reportes;
        }





    }
}
