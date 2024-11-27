using GestionAsistente.AccesoDatos.EntidadesAD;
using GestionAsistente.AccesoDatos.Modelos;
using GestionAsistente.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsistente.ReglasNegocio
{

    public class EstacionTrabajoRN
    {
        private readonly EstacionTrabajoAD estacionTrabajoAD;
        public EstacionTrabajoRN()
        {
            estacionTrabajoAD = new EstacionTrabajoAD();
        }

        public async Task<bool> RegistrarEstacion(EstacionTrabajo estacion)
        {
            if (estacion == null)
            {
                throw new ArgumentNullException(nameof(estacion));
            }
            return await estacionTrabajoAD.RegistrarEstacionTrabajo(estacion); // Sin await si no es asíncrono
        }

        public async Task<int> estacionesPorOficina(int OficinaID)
        {
            if (OficinaID == null)
            {
                throw new ArgumentNullException(nameof(OficinaID));
            }
            return await estacionTrabajoAD.estacionesPorOficina(OficinaID); // Sin await si no es asíncrono
        }

        public async Task<bool> EliminarEstacionPorOficina(int OficinaID)
        {
            if (OficinaID == null)
            {
                throw new ArgumentNullException(nameof(OficinaID));
            }
            return await estacionTrabajoAD.EliminarEstacionPorOficina(OficinaID); // Sin await si no es asíncrono
        }
        public async Task<List<EstacionTrabajo>> ListarEstacionPorOficina(int OficinaID) {
            return await estacionTrabajoAD.ListarEstacionPorOficina(OficinaID);
        }


    }
}





