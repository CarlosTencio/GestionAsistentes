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
    public class HistorialAccionesAD
    {
        private readonly GestionAsistenteContexto _contexto;
        public HistorialAccionesAD()
        {
            this._contexto = new GestionAsistenteContexto();
        }
        public async Task<bool> RegistrarHistorialAcciones(HistorialAcciones historialAcciones)
        {
            HistorialAccionesEF historialAccionesEF = new HistorialAccionesEF
            {
                Fecha = historialAcciones.Fecha,
                NombrePersona = historialAcciones.NombrePersona,
                Accion = historialAcciones.Accion 
            
            };
            this._contexto.HistoriaAccionesEFs.Add(historialAccionesEF);

            return this._contexto.SaveChanges() > 0;
        }
        public List<HistorialAcciones> listarHistorialAcciones(DateTime fecha)
        {
            List<HistorialAccionesEF> historialAccionesEFs = _contexto.HistoriaAccionesEFs.Where(a => a.Fecha == fecha).ToList();
            List<HistorialAcciones> historialAcciones = new List<HistorialAcciones>();

            foreach (HistorialAccionesEF historialAccionesEF in historialAccionesEFs)
            {
                historialAcciones.Add(new HistorialAcciones
                {
                    Fecha = historialAccionesEF.Fecha,
                    NombrePersona = historialAccionesEF.NombrePersona,
                    Accion = historialAccionesEF.Accion
                });
            }

            return historialAcciones;
        }
        public List<HistorialAcciones> BuscarAccionPorPersona(string NombrePersona)
        {
            List<HistorialAccionesEF> historialAccionesEFs = _contexto.HistoriaAccionesEFs.Where(a => a.NombrePersona==NombrePersona).ToList();
            List<HistorialAcciones> historialAcciones = new List<HistorialAcciones>();

            foreach (HistorialAccionesEF historialAccionesEF in historialAccionesEFs)
            {
                historialAcciones.Add(new HistorialAcciones
                {
                    Fecha = historialAccionesEF.Fecha,
                    NombrePersona = historialAccionesEF.NombrePersona,
                    Accion = historialAccionesEF.Accion
                });
            }

            return historialAcciones;
        }
        public List<HistorialAcciones> BuscarPorAccion(string accion)
        {
            var historialAccionesEFs = _contexto.HistoriaAccionesEFs
                .Where(a => a.Accion.Contains(accion)) // Usa Contains para buscar palabras parciales
                .OrderByDescending(a => a.Fecha) // Ordena de reciente a antiguo
                .ToList();

            var historialAcciones = historialAccionesEFs.Select(historialAccionesEF => new HistorialAcciones
            {
                Fecha = historialAccionesEF.Fecha,
                NombrePersona = historialAccionesEF.NombrePersona,
                Accion = historialAccionesEF.Accion
            }).ToList();

            return historialAcciones;
        }
        /*Nuevo*/
        public async Task<List<HistorialAcciones>> ListarHistorial()
        {
            var historialAccionesEFs = _contexto.HistoriaAccionesEFs
                .OrderByDescending(a => a.Fecha) // Ordena por fecha descendente
                .ToList();

            var historialAcciones = historialAccionesEFs.Select(historialAccionesEF => new HistorialAcciones
            {
                HistoriaAccionesID = historialAccionesEF.HistoriaAccionesID,
                NombrePersona = historialAccionesEF.NombrePersona,
                Accion = historialAccionesEF.Accion,
                Fecha = historialAccionesEF.Fecha,
            }).ToList();

            return historialAcciones;
        }

        public async Task LimpiarHistorialCompleto()
        {
            var todosLosRegistros = _contexto.HistoriaAccionesEFs.ToList();
            _contexto.HistoriaAccionesEFs.RemoveRange(todosLosRegistros);

            await _contexto.SaveChangesAsync();
        }

        public List<HistorialAcciones> BuscarHistorial(string persona, string accion, DateTime? fecha)
        {
            var query = _contexto.HistoriaAccionesEFs.AsQueryable();

            if (!string.IsNullOrEmpty(persona))
            {
                query = query.Where(h => h.NombrePersona.Contains(persona));
            }

            if (!string.IsNullOrEmpty(accion))
            {
                query = query.Where(h => h.Accion.Contains(accion));
            }

            if (fecha.HasValue)
            {
                query = query.Where(h => h.Fecha.Date == fecha.Value.Date);
            }

            var historialAccionesEFs = query
                .OrderByDescending(h => h.Fecha) // Ordena de reciente a antiguo
                .ToList();

            var historialAcciones = historialAccionesEFs.Select(historialAccionesEF => new HistorialAcciones
            {
                Fecha = historialAccionesEF.Fecha,
                NombrePersona = historialAccionesEF.NombrePersona,
                Accion = historialAccionesEF.Accion
            }).ToList();

            return historialAcciones;
        }
        public async Task<List<string>> ListarPersonas()
        {
            return await _contexto.HistoriaAccionesEFs
                .Select(a => a.NombrePersona) 
                .Distinct()                   
                .OrderBy(nombre => nombre)    
                .ToListAsync();               
        }


    }
}
