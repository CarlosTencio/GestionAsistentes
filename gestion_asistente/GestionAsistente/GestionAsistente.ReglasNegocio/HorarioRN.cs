using GestionAsistente.AccesoDatos.EntidadesAD;
using GestionAsistente.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsistente.ReglasNegocio
{
    public class HorarioRN
    {
        private readonly HorarioAD horarioAD;
        public HorarioRN()
        {
            horarioAD = new HorarioAD();
        }
        public async Task<bool> RegistrarHorario(Horario horario)
        {
            if (horario == null)
            {
                throw new ArgumentNullException(nameof(horario));
            }
            return await horarioAD.RegistrarHorario(horario); // Sin await si no es asíncrono
        }
        public async Task<List<Horario>> ListarHorariosPorEstacionTrabajo(int estacionTrabajoID)
        {
            return await horarioAD.ListarHorariosPorEstacionTrabajo(estacionTrabajoID);
        }
        public async Task<List<Horario>> ListarHorariosPorOficina(int oficinaID)
        {
            return await horarioAD.ListarHorariosPorOficina(oficinaID);
        }
        public async Task<bool> limpiarHararioEstacion(int estacionTrabajoID)
        {
            return await horarioAD.limpiarHararioEstacion(estacionTrabajoID);
        }
        public async Task<bool> limpiarHorarioAsistente(int asistenteID)
        {
            return await horarioAD.limpiarHorarioAsistente(asistenteID);
        }
        public async Task<bool> limpiarHararioOficina(int oficinaID)
        {
            return await horarioAD.limpiarHorarioOficina(oficinaID);

        }

    }

    }
