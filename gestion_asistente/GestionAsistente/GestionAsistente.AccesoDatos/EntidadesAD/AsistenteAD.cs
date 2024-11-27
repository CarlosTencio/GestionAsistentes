using GestionAsistente.AccesoDatos.Contexto;
using GestionAsistente.AccesoDatos.Modelos;
using GestionAsistente.Entidades;
using Microsoft.EntityFrameworkCore;

namespace GestionAsistente.AccesoDatos.EntidadesAD
{
    public class AsistenteAD
    {
        private readonly GestionAsistenteContexto _contexto;
        private readonly BadgeAD badgeAD;
        private readonly ReporteBadgeAD reporteBadgeAD;
        public AsistenteAD()
        {
            this._contexto = new GestionAsistenteContexto();
            this.reporteBadgeAD = new ReporteBadgeAD();
        }
        public async Task<bool> RegistrarAsistente(Asistente asistente)
        {
            PersonaEF personaEF = new PersonaEF
            {
                Nombre = asistente.Persona.Nombre,
                PrimerApellido = asistente.Persona.PrimerApellido,
                SegundoApellido = asistente.Persona.SegundoApellido
            };
            AsistenteEF asistenteEF = new AsistenteEF
            {
                Persona = personaEF,
                UnidadID = asistente.UnidadID,
                nombreUsuario = asistente.nombreUsuario,
                Accesos = asistente.Accesos,
                EncargadoID = asistente.EncargadoID,
                Contrasenia = asistente.Contrasenia,
                BadgeID = asistente.BadgeID
            };
            BadgeEF badgeEF = _contexto.BadgesEF.Find(asistente.BadgeID);
            if (asistente.BadgeID != null)
            {
                badgeEF.Ocupado = true;

            }

            this._contexto.AsistenteEFs.Add(asistenteEF);
            return this._contexto.SaveChanges() > 0;
        }
        public async Task<List<Asistente>> listarAsistentes()
        {
            try
            {
                List<AsistenteEF> asistenteEFs = await _contexto.AsistenteEFs
                    .Include(a => a.Persona)
                    .Include(a => a.Encargado)
                    .Include(a => a.Encargado.Persona)
                    .Include(a => a.Unidad)
                    .Include(a => a.Badge)
                    .ToListAsync(); // Usar versión asíncrona

                List<Asistente> asistentes = new List<Asistente>();

                foreach (AsistenteEF asistenteEF in asistenteEFs)
                {
                    if (asistenteEF == null) continue;

                    var asistente = new Asistente
                    {
                        AsistenteID = asistenteEF.AsistenteID,
                        EncargadoID = asistenteEF.Encargado?.EncargadoID,
                        nombreUsuario = asistenteEF.nombreUsuario,
                        Accesos = asistenteEF.Accesos,
                        Contrasenia = asistenteEF.Contrasenia,
                        BadgeID = asistenteEF.BadgeID,
                        UnidadID = asistenteEF.UnidadID,

                        // Mapeo seguro de Persona
                        Persona = asistenteEF.Persona != null ? new Persona
                        {
                            PersonaID = asistenteEF.Persona.PersonaID,
                            Nombre = asistenteEF.Persona.Nombre ?? string.Empty,
                            PrimerApellido = asistenteEF.Persona.PrimerApellido ?? string.Empty,
                            SegundoApellido = asistenteEF.Persona.SegundoApellido ?? string.Empty
                        } : null,

                        // Mapeo seguro de Unidad
                        Unidad = asistenteEF.Unidad != null ? new Unidad
                        {
                            UnidadID = asistenteEF.Unidad.UnidadID,
                            Nombre = asistenteEF.Unidad.Nombre ?? string.Empty
                        } : null,

                        // Mapeo seguro de Encargado
                        Encargado = asistenteEF.Encargado != null && asistenteEF.Encargado.Persona != null ?
                            new Encargado
                            {
                                EncargadoID = asistenteEF.Encargado.EncargadoID,
                                Persona = new Persona
                                {
                                    Nombre = asistenteEF.Encargado.Persona.Nombre ?? string.Empty,
                                    PrimerApellido = asistenteEF.Encargado.Persona.PrimerApellido ?? string.Empty,
                                    SegundoApellido = asistenteEF.Encargado.Persona.SegundoApellido ?? string.Empty
                                }
                            } : null
                    };

                    asistentes.Add(asistente);
                }

                return asistentes;
            }
            catch (Exception ex)
            {
                // Aquí puedes manejar la excepción como prefieras
                throw new Exception($"Error al listar asistentes: {ex.Message}");
            }
        }

        public async Task<List<Asistente>> ListarAsistentesPorUnidadID(int? unidad)
        {
            Console.WriteLine($"Filtrando asistentes con UnidadID: {unidad}");

            var asistentesFiltrados = await _contexto.AsistenteEFs
                .Where(a => a.UnidadID == unidad)
                .ToListAsync();

            // Mapea los objetos solo después del filtro en la base de datos
            List<Asistente> asistentes = asistentesFiltrados.Select(a => new Asistente
            {
                AsistenteID = a.AsistenteID,
                EncargadoID = a.Encargado?.EncargadoID,
                nombreUsuario = a.nombreUsuario,
                Accesos = a.Accesos,
                Contrasenia = a.Contrasenia,
                BadgeID = a.BadgeID,
                UnidadID = a.UnidadID,
                Persona = a.Persona != null ? new Persona
                {
                    PersonaID = a.Persona.PersonaID,
                    Nombre = a.Persona.Nombre ?? string.Empty,
                    PrimerApellido = a.Persona.PrimerApellido ?? string.Empty,
                    SegundoApellido = a.Persona.SegundoApellido ?? string.Empty
                } : null,
                Unidad = a.Unidad != null ? new Unidad
                {
                    UnidadID = a.Unidad.UnidadID,
                    Nombre = a.Unidad.Nombre ?? string.Empty
                } : null,
                Encargado = a.Encargado != null && a.Encargado.Persona != null ?
                            new Encargado
                            {
                                EncargadoID = a.Encargado.EncargadoID,
                                Persona = new Persona
                                {
                                    Nombre = a.Encargado.Persona.Nombre ?? string.Empty,
                                    PrimerApellido = a.Encargado.Persona.PrimerApellido ?? string.Empty,
                                    SegundoApellido = a.Encargado.Persona.SegundoApellido ?? string.Empty
                                }
                            } : null
            }).ToList();

            return asistentes;
        }

        public async Task<bool> ModificarAsistente(Asistente asistente)
        {
            // Buscar el asistente por ID
            var asistenteEF = await _contexto.AsistenteEFs
                .FirstOrDefaultAsync(a => a.AsistenteID == asistente.AsistenteID);
            if (asistenteEF.BadgeID != null)
            {
                int? idBadge = asistenteEF.BadgeID;
                BadgeEF badgeEF = _contexto.BadgesEF.Find(idBadge);
                badgeEF.Ocupado = false;


            }

            // Verificar si se encontró el asistente
            if (asistenteEF == null)
            {
                return false; // Si no se encontró, retorna falso
            }

            PersonaEF personaEF = this._contexto.PersonaEFs
                 .FirstOrDefault(p => p.PersonaID == asistente.Persona.PersonaID);
            if (personaEF == null)
            {
                return false;
            }
            personaEF.Nombre = asistente.Persona.Nombre;
            personaEF.PrimerApellido = asistente.Persona.PrimerApellido;
            personaEF.SegundoApellido = asistente.Persona.SegundoApellido;

            // Actualizar los demás campos del asistente
            asistenteEF.UnidadID = asistente.UnidadID;
            asistenteEF.nombreUsuario = asistente.nombreUsuario;
            asistenteEF.Accesos = asistente.Accesos;
            asistenteEF.EncargadoID = asistente.EncargadoID;
            asistenteEF.Contrasenia = asistente.Contrasenia;
            asistenteEF.BadgeID = asistente.BadgeID;

            // Guardar los cambios en la base de datos
            BadgeEF badgeEFActual = _contexto.BadgesEF.Find(asistente.BadgeID);

            if (badgeEFActual != null)
            {


                badgeEFActual.Ocupado = true;

            }

            return await _contexto.SaveChangesAsync() > 0; // Asegúrate de usar SaveChangesAsync para mantener la asynchronía
        }
        public async Task<bool> EliminarAsistente(int? AsistenteID)
        {
            AsistenteEF asistenteEF = _contexto.AsistenteEFs.Find(AsistenteID);
            if (asistenteEF == null)
            {
                return false;
            }
            if (asistenteEF.BadgeID != null)
            {
                BadgeEF badgeEF = _contexto.BadgesEF.Find(asistenteEF.BadgeID);
                if (badgeEF != null)
                {
                    badgeEF.Ocupado = false;
                    _contexto.BadgesEF.Update(badgeEF);
                }
            }
            _contexto.AsistenteEFs.Remove(asistenteEF);
            return await _contexto.SaveChangesAsync() > 0;
        }
        public async Task<List<Asistente>> BuscarAsistentePorNombre(string nombre)
        {
            // Validar que el nombre no esté vacío o nulo
            if (string.IsNullOrWhiteSpace(nombre))
            {
                return new List<Asistente>();
            }

            // Dividir el término de búsqueda en palabras individuales y eliminar espacios en blanco
            var terminosBusqueda = nombre.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(t => t.Trim().ToLower())
                .ToList();

            var asistentesEF = await _contexto.AsistenteEFs
                .Include(a => a.Badge)
                .Include(a => a.Unidad)
                .Include(a => a.Persona)
                .Include(a => a.Encargado)
                    .ThenInclude(e => e.Persona)
                .Where(a => terminosBusqueda.Any(termino =>
                    (a.Persona.Nombre != null && a.Persona.Nombre.ToLower().Contains(termino)) ||
                    (a.Persona.PrimerApellido != null && a.Persona.PrimerApellido.ToLower().Contains(termino)) ||
                    (a.Persona.SegundoApellido != null && a.Persona.SegundoApellido.ToLower().Contains(termino))))
                .ToListAsync();

            return asistentesEF.Select(asistenteEF => new Asistente
            {
                AsistenteID = asistenteEF.AsistenteID,
                EncargadoID = asistenteEF.Encargado?.EncargadoID,
                nombreUsuario = asistenteEF.nombreUsuario,
                Accesos = asistenteEF.Accesos,
                Contrasenia = asistenteEF.Contrasenia,
                BadgeID = asistenteEF.BadgeID,
                UnidadID = asistenteEF.UnidadID,
                Persona = asistenteEF.Persona != null ? new Persona
                {
                    PersonaID = asistenteEF.Persona.PersonaID,
                    Nombre = asistenteEF.Persona.Nombre ?? string.Empty,
                    PrimerApellido = asistenteEF.Persona.PrimerApellido ?? string.Empty,
                    SegundoApellido = asistenteEF.Persona.SegundoApellido ?? string.Empty
                } : null,
                Unidad = asistenteEF.Unidad != null ? new Unidad
                {
                    UnidadID = asistenteEF.Unidad.UnidadID,
                    Nombre = asistenteEF.Unidad.Nombre ?? string.Empty
                } : null,
                Encargado = asistenteEF.Encargado != null && asistenteEF.Encargado.Persona != null ?
                            new Encargado
                            {
                                EncargadoID = asistenteEF.Encargado.EncargadoID,
                                Persona = new Persona
                                {
                                    Nombre = asistenteEF.Encargado.Persona.Nombre ?? string.Empty,
                                    PrimerApellido = asistenteEF.Encargado.Persona.PrimerApellido ?? string.Empty,
                                    SegundoApellido = asistenteEF.Encargado.Persona.SegundoApellido ?? string.Empty
                                }
                            } : null,
            }).ToList();
        }
        public List<Asistente> BuscarAsistentePorEncargado(string encargado)
        {
            var asistentesEF = _contexto.AsistenteEFs
        .Include(a => a.Badge)
        .Include(a => a.Unidad)
        .Include(a => a.Encargado)
            .ThenInclude(e => e.Persona)
        .Where(a => a.Encargado != null && a.Encargado.Persona != null && a.Encargado.Persona.Nombre == encargado)
        .ToList();

            if (asistentesEF == null || !asistentesEF.Any())
            {
                return new List<Asistente>();
            }


            return asistentesEF.Select(asistenteEF => new Asistente
            {
                UnidadID = asistenteEF.UnidadID,
                Accesos = asistenteEF.Accesos,
                Contrasenia = asistenteEF.Contrasenia,
                BadgeID = asistenteEF.BadgeID,
                nombreUsuario = asistenteEF.nombreUsuario,
                Badge = asistenteEF.Badge != null ? new Badge
                {
                    BadgeID = asistenteEF.Badge.BadgeID,
                    Activo = asistenteEF.Badge.Activo,
                    Justificacion = asistenteEF.Badge.Justificacion,
                    Horario = asistenteEF.Badge?.Horario,
                    Unidad = asistenteEF.Badge?.Unidad != null ? new Unidad
                    {
                        UnidadID = asistenteEF.Badge.Unidad.UnidadID,
                        Nombre = asistenteEF.Badge.Unidad.Nombre,
                    } : null
                } : null,
                Unidad = asistenteEF.Unidad != null ? new Unidad
                {
                    UnidadID = asistenteEF.Unidad.UnidadID,
                    Nombre = asistenteEF.Unidad.Nombre,
                } : null,
                Encargado = asistenteEF.Encargado != null ? new Encargado
                {
                    Persona = new Persona
                    {
                        Nombre = asistenteEF.Encargado.Persona.Nombre,
                        PrimerApellido = asistenteEF.Encargado.Persona.PrimerApellido,
                        SegundoApellido = asistenteEF.Encargado.Persona.SegundoApellido
                    }
                } : null
            }).ToList();
        }

    }
}
