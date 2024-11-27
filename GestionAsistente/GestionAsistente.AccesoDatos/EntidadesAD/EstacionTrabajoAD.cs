using GestionAsistente.AccesoDatos.Contexto;
using GestionAsistente.AccesoDatos.Modelos;
using GestionAsistente.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsistente.AccesoDatos.EntidadesAD
{
    public class EstacionTrabajoAD
    {
        private readonly GestionAsistenteContexto _contexto;
        public EstacionTrabajoAD()
        {
            this._contexto = new GestionAsistenteContexto();
        }
        public async Task<bool> RegistrarEstacionTrabajo(EstacionTrabajo estacionTrabajo)
        {
            EstacionTrabajoEF estacionTrabajoEF = new EstacionTrabajoEF
            {
                Numero = estacionTrabajo.Numero,
                Estado = estacionTrabajo.Estado,
                OficinaID = estacionTrabajo.OficinaID
            };
            this._contexto.EstacionTrabajoEFs.Add(estacionTrabajoEF);
            return this._contexto.SaveChanges() > 0;
        }

        public async Task<(string, bool)> ActualizarEstacion(Oficina oficina)
        {
            OficinaEF oficinaEF = _contexto.OficinaEFs.Find(oficina.OficinaID);
            if (oficinaEF == null)
            {
                return ("La oficina no se pudo actualizar", false);
            }
            oficinaEF.Nombre = oficina.Nombre;
            return this._contexto.SaveChanges() > 0
        ? ("Actualizado correctamente", true)
        : ("Error al actualizar", false);
        }

        public List<EstacionTrabajo> listarEstacionesTrabajo()
        {
            List<EstacionTrabajoEF> estacionTrabajoEFs = _contexto.EstacionTrabajoEFs.ToList();
            List<EstacionTrabajo> estacionesTrabajo = new List<EstacionTrabajo>();

            foreach (EstacionTrabajoEF estacionTrabajoEF in estacionTrabajoEFs)
            {
                estacionesTrabajo.Add(new EstacionTrabajo
                {
                    Numero = estacionTrabajoEF.Numero,
                    Estado = estacionTrabajoEF.Estado,
                    OficinaID = estacionTrabajoEF.OficinaID
                });
            }

            return estacionesTrabajo;
        }

        public async Task<int> estacionesPorOficina(int oficinaID)
        {
            List<EstacionTrabajoEF> estacionTrabajoEFs = _contexto.EstacionTrabajoEFs.ToList();
            List<EstacionTrabajo> estacionesTrabajo = new List<EstacionTrabajo>();
            int cantidadEstaciones = 0;

            foreach (EstacionTrabajoEF estacionTrabajoEF in estacionTrabajoEFs)
            {
                estacionesTrabajo.Add(new EstacionTrabajo
                {
                    Numero = estacionTrabajoEF.Numero,
                    Estado = estacionTrabajoEF.Estado,
                    OficinaID = estacionTrabajoEF.OficinaID
                });

                if (oficinaID == estacionTrabajoEF.OficinaID)
                {
                    cantidadEstaciones++;
                }

            }

            return cantidadEstaciones;
        }

        public EstacionTrabajo BuscarEstacion(int numero)
        {
            var estacionTrabajo = _contexto.EstacionTrabajoEFs
              .Where(x => x.Numero == numero)
                .Select(x => new EstacionTrabajo
                {
                    EstacionTrabajoID = x.EstacionTrabajoID,
                    Numero = x.Numero,
                    Estado = x.Estado,
                    OficinaID = x.OficinaID
                }).FirstOrDefault();
            if (estacionTrabajo == null)
            {
                return null;
            }
            return estacionTrabajo;
        }
        public bool EliminarEstacionTrabajo(int EstacionTrabajoID)
        {
            EstacionTrabajoEF estacionTrabajoEF = _contexto.EstacionTrabajoEFs.Find(EstacionTrabajoID);
            if (estacionTrabajoEF == null)
            {
                return false;
            }
            _contexto.EstacionTrabajoEFs.Remove(estacionTrabajoEF);
            return _contexto.SaveChanges() > 0;
        }


        public async Task<bool> EliminarEstacionPorOficina(int OficinaID)
        {
            EstacionTrabajoEF estacionTrabajoEF = _contexto.EstacionTrabajoEFs.Find(OficinaID);
            int cantidadEstaciones = await estacionesPorOficina(OficinaID);
            if (estacionTrabajoEF == null)
            {
                return false;
            }
            while (estacionTrabajoEF.OficinaID.Equals(OficinaID))
            {
                _contexto.EstacionTrabajoEFs.Remove(estacionTrabajoEF);
            }
            
            return _contexto.SaveChanges() > 0;
        }
        public async Task<List<EstacionTrabajo>> ListarEstacionPorOficina(int OficinaID)
        {
            List<EstacionTrabajo> estacionTrabajo = new List<EstacionTrabajo>();
            List<EstacionTrabajoEF> estacionTrabajos = new List<EstacionTrabajoEF>();
            estacionTrabajos = _contexto.EstacionTrabajoEFs.Where(e => e.OficinaID == OficinaID).ToList();
            foreach (EstacionTrabajoEF estacionTrabajoEF in estacionTrabajos)
            {
                estacionTrabajo.Add(new EstacionTrabajo
                {
                    EstacionTrabajoID = estacionTrabajoEF.EstacionTrabajoID,
                    Numero = estacionTrabajoEF.Numero,
                    Estado = estacionTrabajoEF.Estado,
                    OficinaID = estacionTrabajoEF.OficinaID
                });
            }
            return estacionTrabajo;
        }
    }
}
