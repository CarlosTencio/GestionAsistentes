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
    public class BadgeAD
    {
        private readonly GestionAsistenteContexto _contexto;
        public BadgeAD()
        {
            this._contexto = new GestionAsistenteContexto();
        }
        public async Task<bool> RegistrarBadge(Badge badge)
        {
            BadgeEF badgeEF = new BadgeEF
            {
                Activo = badge.Activo,
                Justificacion = badge.Justificacion,
                Horario = badge.Horario,
                UnidadID = badge.UnidadID,
                Ocupado = badge.Ocupado
            };
            this._contexto.BadgesEF.Add(badgeEF);
            return this._contexto.SaveChanges() > 0;
        }

        public async Task<List<Badge>> ListarBadge()
        {
            List<Badge> badges = new List<Badge>();
            List<BadgeEF> badgesEF = this._contexto.BadgesEF
                .Include(e => e.Unidad)  // Incluir Unidad
                .ToList();

            foreach (BadgeEF badgeEF in badgesEF)
            {

                Badge badge = new Badge
                {
                    BadgeID = badgeEF.BadgeID,
                    Horario = badgeEF.Horario,
                    UnidadID = badgeEF.UnidadID,
                    Justificacion = badgeEF.Justificacion,
                    Activo = badgeEF.Activo,
                    Ocupado = badgeEF.Ocupado,
                    Unidad = badgeEF.Unidad != null ? new Unidad
                    {
                        UnidadID = badgeEF.Unidad.UnidadID,
                        Nombre = badgeEF.Unidad.Nombre
                    } : null
                };

                badges.Add(badge);
            }

            return badges;
        }

        public async Task<List<Badge>> BuscarBadgesPorNumero(int numBadge)
        {
            return await _contexto.BadgesEF
                .Where(b => b.BadgeID == numBadge) // Asegúrate de que sea el campo correcto
                .Select(b => new Badge
                {
                    BadgeID = b.BadgeID,
                    Horario = b.Horario,
                    UnidadID = b.UnidadID,
                    Justificacion = b.Justificacion,
                    Activo = b.Activo,
                    Ocupado = b.Ocupado,
                    Unidad = b.Unidad != null ? new Unidad
                    {
                        UnidadID = b.Unidad.UnidadID,
                        Nombre = b.Unidad.Nombre
                    } : null
                })
                .ToListAsync();
        }

        public async Task<List<Badge>> BuscarBadgesPorNombreUnidad(string nombreUnidad)
        {
            return await _contexto.BadgesEF
                .Where(b => b.Unidad != null && b.Unidad.Nombre.Contains(nombreUnidad)) // Búsqueda por nombre de la unidad
                .Select(b => new Badge
                {
                    BadgeID = b.BadgeID,
                    Horario = b.Horario,
                    UnidadID = b.UnidadID,
                    Justificacion = b.Justificacion,
                    Activo = b.Activo,
                    Ocupado = b.Ocupado,
                    Unidad = b.Unidad != null ? new Unidad
                    {
                        UnidadID = b.Unidad.UnidadID,
                        Nombre = b.Unidad.Nombre
                    } : null
                })
                .ToListAsync();
        }



        public async Task<bool> ModificarBadge(Badge badge)
        {
            BadgeEF badgeEF = _contexto.BadgesEF.FirstOrDefault(b => b.BadgeID == badge.BadgeID);

            if (badgeEF == null)
            {
                return false;
            }

            //si el badge que viene esta activo, pero el que estaba previamente tiene algo de justificacion entonces se debe quitar ya que se logro activar
            if (badge.Activo == true && !string.IsNullOrEmpty(badgeEF.Justificacion))
            {
                badge.Justificacion = "";
            }
            if (!badge.Activo)
            {
                _contexto.AsistenteEFs.Where(a => a.BadgeID == badge.BadgeID).ToList().ForEach(a => a.BadgeID = null);
            }

            badgeEF.BadgeID = badge.BadgeID;
            badgeEF.Activo = badge.Activo;
            badgeEF.Justificacion = badge.Justificacion;
            badgeEF.Horario = badge.Horario;
            badgeEF.UnidadID = badge.UnidadID;
            badgeEF.Ocupado = badge.Ocupado;

            return _contexto.SaveChanges() > 0;
        }
        public async Task<bool> EliminarBadge(int BadgeID)
        {
            BadgeEF badgeEF = this._contexto.BadgesEF.FirstOrDefault(b => b.BadgeID == BadgeID);
            if (badgeEF == null)
            {
                return false;
            }
            this._contexto.BadgesEF.Remove(badgeEF);
            return _contexto.SaveChanges() > 0;
        }
        public async Task<List<Badge>> listarBadgePorUnidad(int? unidadId)
        {
            // Filtrar los badges por UnidadID antes de traerlos desde la base de datos
            List<BadgeEF> badgesEF = await this._contexto.BadgesEF
                .Include(e => e.Unidad)  // Incluir Unidad
                .Where(b => b.UnidadID == unidadId && b.Ocupado != true && b.Activo == true)  // Filtrar por UnidadID
                .ToListAsync();

            List<Badge> badges = new List<Badge>();

            foreach (BadgeEF badgeEF in badgesEF)
            {
                Badge badge = new Badge
                {
                    BadgeID = badgeEF.BadgeID,
                    Horario = badgeEF.Horario,
                    UnidadID = badgeEF.UnidadID,
                    Justificacion = badgeEF.Justificacion,
                    Activo = badgeEF.Activo,
                    Unidad = badgeEF.Unidad != null ? new Unidad
                    {
                        UnidadID = badgeEF.Unidad.UnidadID,
                        Nombre = badgeEF.Unidad.Nombre
                    } : null
                };

                badges.Add(badge);
            }

            return badges;
        }
        public async Task<Badge> ObtenerBadgePorId(int? badgeID)
        {
            // Buscar un BadgeEF que coincida con el badgeID
            BadgeEF badgeEF = await this._contexto.BadgesEF
                .Include(e => e.Unidad)  // Incluir Unidad
                .FirstOrDefaultAsync(b => b.BadgeID == badgeID);  // Filtrar por BadgeID

            // Verificar si se encontró el badge
            if (badgeEF == null)
            {
                return null; // o puedes lanzar una excepción si prefieres
            }

            // Mapear BadgeEF a Badge
            Badge badge = new Badge
            {
                BadgeID = badgeEF.BadgeID,
                Horario = badgeEF.Horario,
                UnidadID = badgeEF.UnidadID,
                Justificacion = badgeEF.Justificacion,
                Activo = badgeEF.Activo,
                Unidad = badgeEF.Unidad != null ? new Unidad
                {
                    UnidadID = badgeEF.Unidad.UnidadID,
                    Nombre = badgeEF.Unidad.Nombre
                } : null
            };

            return badge;
        }
    }
}
