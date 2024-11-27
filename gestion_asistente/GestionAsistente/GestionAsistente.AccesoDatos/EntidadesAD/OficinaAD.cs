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
    public class OficinaAD
    {
        private readonly GestionAsistenteContexto _contexto;
        public OficinaAD()
        {
            _contexto = new GestionAsistenteContexto();
        }
        public List<OficinaEF> ObtenerOficinas()
        {
            return _contexto.OficinaEFs.ToList();
        }
        public async Task<int> RegistrarOficina(Oficina oficina)
        {
            OficinaEF oficinaEF = new OficinaEF
            {
                Nombre = oficina.Nombre
            };
            _contexto.OficinaEFs.Add(oficinaEF);
            _contexto.SaveChanges();
            return oficinaEF.OficinaID;
            //obtener el ID del ultimo
        }
        public async Task<bool> ExisteOficina(string nombre)
        {
            var oficina = _contexto.OficinaEFs
              .Where(x => x.Nombre == nombre)
                .Select(x => new Oficina
                {
                    OficinaID = x.OficinaID,
                    Nombre = x.Nombre
                }).FirstOrDefault();
            if (oficina == null)
            {
                return false;
            }
            return true;
        }
        public async Task<(string, bool)> ModificarOficina(Oficina oficina, int cantidadEstaciones)
        {
            // Buscar la oficina en la base de datos
            OficinaEF oficinaEF = await _contexto.OficinaEFs.FindAsync(oficina.OficinaID);
            if (oficinaEF == null)
            {
                return ("La oficina no se pudo actualizar", false);
            }

            // Actualizar el nombre de la oficina
            oficinaEF.Nombre = oficina.Nombre;

            // Obtener estaciones de trabajo actuales asociadas a esta oficina
            var estacionesActuales = _contexto.EstacionTrabajoEFs
                .Where(et => et.OficinaID == oficina.OficinaID)
                .OrderBy(et => et.Numero) // Ordenar por número de estación
                .ToList();

            int cantidadActual = estacionesActuales.Count;

            if (cantidadEstaciones > cantidadActual)
            {
                // Agregar nuevas estaciones de trabajo para completar la cantidad deseada
                for (int i = cantidadActual + 1; i <= cantidadEstaciones; i++)
                {
                    estacionesActuales.Add(new EstacionTrabajoEF
                    {
                        Numero = i,
                        Estado = 0, // Estado inicial
                        OficinaID = oficina.OficinaID
                    });
                }
                _contexto.EstacionTrabajoEFs.AddRange(estacionesActuales.Skip(cantidadActual));
            }
            else if (cantidadEstaciones < cantidadActual)
            {
                // Eliminar las estaciones de trabajo en exceso
                var estacionesAEliminar = estacionesActuales
                    .Skip(cantidadEstaciones)
                    .ToList();

                _contexto.EstacionTrabajoEFs.RemoveRange(estacionesAEliminar);
                estacionesActuales = estacionesActuales.Take(cantidadEstaciones).ToList();
            }

            // Reasignar los números de las estaciones de trabajo para que sean consecutivos
            for (int i = 0; i < estacionesActuales.Count; i++)
            {
                estacionesActuales[i].Numero = i + 1;
            }

            // Guardar cambios en la base de datos
            bool guardadoExitoso = await _contexto.SaveChangesAsync() > 0;

            return guardadoExitoso
                ? ("Oficina y estaciones de trabajo actualizadas correctamente", true)
                : ("Error al actualizar la oficina y estaciones de trabajo", false);
        }
        public async Task<bool> EliminarOficina(int OficinaID)
        {
            OficinaEF oficinaEF = _contexto.OficinaEFs.Find(OficinaID);
            if (oficinaEF == null)
            {
                return false;
            }
            _contexto.OficinaEFs.Remove(oficinaEF);
            return _contexto.SaveChanges() > 0;
        }
    }
}
