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
    public class HorarioAD
    {
        private readonly GestionAsistenteContexto _contexto;
        public HorarioAD()
        {
            _contexto = new GestionAsistenteContexto();
        }
        public async Task<bool> RegistrarHorario(Horario horario)
        {
            HorarioEF horarioEF = new HorarioEF
            {
                AsistenteID = horario.Asistente.AsistenteID,
                HoraInicio = horario.HoraInicio,
                HoraFin = horario.HoraFin,
                Dia = horario.Dia,
                EstacionTrabajoID = horario.EstacionTrabajoID
            };
            _contexto.HorarioEFs.Add(horarioEF);
            return _contexto.SaveChanges() > 0;
        }
        public Horario BuscarHorarioPorAsistente(int AsistenteID)
        {
            var horario = _contexto.HorarioEFs
              .Where(x => x.AsistenteID == AsistenteID)
                .Select(x => new Horario
                {
                    HorarioID = x.HorarioID,
                    HoraInicio = x.HoraInicio,
                    HoraFin = x.HoraFin,
                    Dia = x.Dia,
                    EstacionTrabajoID = x.EstacionTrabajoID
                }).FirstOrDefault();
            if (horario == null)
            {
                return null;
            }
            return horario;
        }
        public List<Horario> ListarHorarios()
        {
            return _contexto.HorarioEFs.Select(x => new Horario
            {
                HorarioID = x.HorarioID,
                HoraInicio = x.HoraInicio,
                HoraFin = x.HoraFin,
                Dia = x.Dia,
                EstacionTrabajoID = x.EstacionTrabajoID,
                AsistenteID = x.AsistenteID,
                Asistente = new Asistente
                {
                    AsistenteID = x.Asistente.AsistenteID,
                    Persona = new Persona
                    {
                        PersonaID = x.Asistente.Persona.PersonaID,
                        Nombre = x.Asistente.Persona.Nombre,
                        PrimerApellido = x.Asistente.Persona.PrimerApellido,
                        SegundoApellido = x.Asistente.Persona.SegundoApellido,
                    },
                },
            }).ToList();
        }
        public async Task<List<Horario>> ListarHorariosPorEstacionTrabajo(int estacionTrabajoID)
        {
            return _contexto.HorarioEFs.Select(x => new Horario
            {
                HorarioID = x.HorarioID,
                HoraInicio = x.HoraInicio,
                HoraFin = x.HoraFin,
                Dia = x.Dia,
                EstacionTrabajoID = x.EstacionTrabajoID,
                EstacionTrabajo = new EstacionTrabajo
                {
                    EstacionTrabajoID = x.EstacionTrabajo.EstacionTrabajoID,
                    OficinaID = x.EstacionTrabajo.OficinaID,
                    Numero = x.EstacionTrabajo.Numero,
                },
                AsistenteID = x.AsistenteID,
                Asistente = new Asistente
                {
                    AsistenteID = x.Asistente.AsistenteID,
                    Persona = new Persona
                    {
                        PersonaID = x.Asistente.Persona.PersonaID,
                        Nombre = x.Asistente.Persona.Nombre,
                        PrimerApellido = x.Asistente.Persona.PrimerApellido,
                        SegundoApellido = x.Asistente.Persona.SegundoApellido,
                    },
                },
            }).Where(h => h.EstacionTrabajoID == estacionTrabajoID).ToList();
        }
        public async Task<List<Horario>> ListarHorariosPorOficina(int oficinaID)
        {
            return await _contexto.HorarioEFs
       .Where(h => h.EstacionTrabajo.OficinaID == oficinaID) // Aplica el filtro primero
       .Include(h => h.EstacionTrabajo)                      // Incluye las relaciones
       .ThenInclude(et => et.Oficina)
       .Include(h => h.Asistente)
       .ThenInclude(a => a.Persona)
       .Select(x => new Horario
       {
           HorarioID = x.HorarioID,
           HoraInicio = x.HoraInicio,
           HoraFin = x.HoraFin,
           Dia = x.Dia,
           EstacionTrabajoID = x.EstacionTrabajoID,
           EstacionTrabajo = new EstacionTrabajo
           {
               EstacionTrabajoID = x.EstacionTrabajo.EstacionTrabajoID,
               OficinaID = x.EstacionTrabajo.OficinaID,
               Numero = x.EstacionTrabajo.Numero,
               Oficina = new Oficina
               {
                   OficinaID = x.EstacionTrabajo.Oficina.OficinaID,
                   Nombre = x.EstacionTrabajo.Oficina.Nombre,
               },
           },
           AsistenteID = x.AsistenteID,
           Asistente = new Asistente
           {
               AsistenteID = x.Asistente.AsistenteID,
               Persona = new Persona
               {
                   PersonaID = x.Asistente.Persona.PersonaID,
                   Nombre = x.Asistente.Persona.Nombre,
                   PrimerApellido = x.Asistente.Persona.PrimerApellido,
                   SegundoApellido = x.Asistente.Persona.SegundoApellido,
               },
           },
       })
       .ToListAsync(); // Usa ToListAsync() para consultas asincrónicas
        }
        public async Task<bool> limpiarHararioEstacion(int estacionTrabajoID)
        {
            var horarios = await _contexto.HorarioEFs
        .Where(h => h.EstacionTrabajoID == estacionTrabajoID)
        .ToListAsync();

            _contexto.HorarioEFs.RemoveRange(horarios);
            return await _contexto.SaveChangesAsync() > 0;
        }
        public async Task<bool> limpiarHorarioAsistente(int asistenteID)
        {
            var horarios = await _contexto.HorarioEFs.Where(h => h.AsistenteID == asistenteID).ToListAsync();
            _contexto.HorarioEFs.RemoveRange(horarios);
            return await _contexto.SaveChangesAsync() > 0;
        }
        public async Task<bool> limpiarHorarioOficina(int oficinaID)
        {
            var horarios = await _contexto.HorarioEFs
                .Where(h => h.EstacionTrabajo.OficinaID == oficinaID)
                .ToListAsync();
            _contexto.HorarioEFs.RemoveRange(horarios);
            return await _contexto.SaveChangesAsync() > 0;

        }
    }
}
