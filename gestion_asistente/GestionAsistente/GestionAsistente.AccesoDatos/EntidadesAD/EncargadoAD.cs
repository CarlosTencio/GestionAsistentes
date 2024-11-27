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
    public class EncargadoAD
    {
        private readonly GestionAsistenteContexto _contexto;
        public EncargadoAD()
        {
            this._contexto = new GestionAsistenteContexto();
        }
        public async Task<(string, bool)> RegistrarEncargado(Encargado encargado)
        {
            // Crear el objeto PersonaEF
            PersonaEF personaEF = new PersonaEF
            {
                Nombre = encargado.Persona.Nombre,
                PrimerApellido = encargado.Persona.PrimerApellido,
                SegundoApellido = encargado.Persona.SegundoApellido
            };

            // Crear el objeto EncargadoEF y asociarlo con la PersonaEF
            EncargadoEF encargadoEF = new EncargadoEF
            {
                Persona = personaEF, // Aquí asignamos el objeto persona
                UnidadID = encargado.Unidad.UnidadID
            };

            // Agregar el objeto EncargadoEF al contexto
            this._contexto.EncargadoEFs.Add(encargadoEF);

            // Guardar los cambios
            return this._contexto.SaveChanges() > 0
? ("Insertado correctamente", true)
: ("Error al insertar", false);
        }
        public async Task<List<Encargado>> ListarEncargados()
        {
            List<Encargado> encargados = new List<Encargado>();
            List<EncargadoEF> encargadosEF = this._contexto.EncargadoEFs
                .Include(e => e.Persona) // Incluir Persona
                .Include(e => e.Unidad)  // Incluir Unidad
                .ToList();

            foreach (EncargadoEF encargadoEF in encargadosEF)
            {
                Encargado encargado = new Encargado
                {
                    EncargadoID = encargadoEF.EncargadoID,
                    PersonaID = encargadoEF.PersonaID,
                    UnidadID = encargadoEF.UnidadID,

                    // Verificar si la entidad Persona no es nula en encargadoEF
                    Persona = encargadoEF.Persona != null ? new Persona
                    {
                        PersonaID = encargadoEF.Persona.PersonaID,
                        Nombre = encargadoEF.Persona.Nombre,
                        PrimerApellido = encargadoEF.Persona.PrimerApellido,
                        SegundoApellido = encargadoEF.Persona.SegundoApellido
                    } : null,

                    // Verificar si la entidad Unidad no es nula en encargadoEF
                    Unidad = encargadoEF.Unidad != null ? new Unidad
                    {
                        UnidadID = encargadoEF.Unidad.UnidadID,
                        Nombre = encargadoEF.Unidad.Nombre
                    } : null
                };

                encargados.Add(encargado);
            }


            return encargados;
        }
        public async Task<List<Encargado>> ListarEncargadosPorID(int? unidadID)
        {
            List<Encargado> encargados = new List<Encargado>();
            List<EncargadoEF> encargadosEF = this._contexto.EncargadoEFs
                .Include(e => e.Persona) // Incluir Persona
                .Include(e => e.Unidad)  // Incluir Unidad
                .Where(e => e.UnidadID == unidadID)
                .ToList();

            foreach (EncargadoEF encargadoEF in encargadosEF)
            {
                Encargado encargado = new Encargado
                {
                    EncargadoID = encargadoEF.EncargadoID,
                    PersonaID = encargadoEF.PersonaID,
                    UnidadID = encargadoEF.UnidadID,
                    Persona = new Persona
                    {
                        PersonaID = encargadoEF.Persona.PersonaID,
                        Nombre = encargadoEF.Persona.Nombre,
                        PrimerApellido = encargadoEF.Persona.PrimerApellido,
                        SegundoApellido = encargadoEF.Persona.SegundoApellido
                    },
                    Unidad = encargadoEF.Unidad != null ? new Unidad
                    {
                        UnidadID = encargadoEF.Unidad.UnidadID,
                        Nombre = encargadoEF.Unidad.Nombre
                    }: null
                };
                encargados.Add(encargado);
            }

            return encargados;
        }
        public async Task<(string, bool)> EliminarEncargado(int encargadoID)
        {
            EncargadoEF encargadoEF = this._contexto.EncargadoEFs
                .FirstOrDefault(e => e.EncargadoID == encargadoID);
            if (encargadoEF == null)
            {
                return ("No existe el registro de encargado", false);
            }
            PersonaEF personaEF = this._contexto.PersonaEFs
                .FirstOrDefault(p => p.PersonaID == encargadoEF.PersonaID);
            if (personaEF == null)
            {
                return ("No existe el registro de encargado", false);
            }
            this._contexto.EncargadoEFs.Remove(encargadoEF);
            this._contexto.PersonaEFs.Remove(personaEF);
            return this._contexto.SaveChanges() > 0
       ? ("Eliminado correctamente", true)
       : ("Error al Eliminar", false);
        }
        public async Task<(string, bool)> ActualizarEncargado(Encargado encargado)
        {
            EncargadoEF encargadoEF = this._contexto.EncargadoEFs
                .FirstOrDefault(e => e.EncargadoID == encargado.EncargadoID);
            if (encargadoEF == null)
            {
                return ("El encargado no se pudo actualizar", false);
            }
            PersonaEF personaEF = this._contexto.PersonaEFs
                .FirstOrDefault(p => p.PersonaID == encargado.PersonaID);
            if (personaEF == null)
            {
                return ("La persona no se pudo actualizar", false);
            }
            personaEF.Nombre = encargado.Persona.Nombre;
            personaEF.PrimerApellido = encargado.Persona.PrimerApellido;
            personaEF.SegundoApellido = encargado.Persona.SegundoApellido;
            encargadoEF.PersonaID = encargado.PersonaID;
            encargadoEF.UnidadID = encargado.Unidad.UnidadID;
            return this._contexto.SaveChanges() > 0
        ? ("Actualizado correctamente", true)
        : ("Error al actualizar", false);
        }
    }
}
