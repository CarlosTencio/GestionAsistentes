using GestionAsistente.AccesoDatos.EntidadesAD;
using GestionAsistente.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionAsistente.ReglasNegocio
{
    public class EncargadoRN
    {
        private readonly EncargadoAD encargadoAD;

        public EncargadoRN()
        {
            encargadoAD = new EncargadoAD();
        }
        public async Task<(string, bool)> RegistrarEncardado(Encargado encargado)
        {
            if (encargado != null)
            {
               
               
                if (encargado.Persona.SegundoApellido == null)
                {
                    return ("El segundo apellido no puede ser nulo", false);
                }
                if (encargado.Persona.PrimerApellido == null)
                {
                    return ("El primer apellido no puede ser nulo", false);
                }
                if (encargado.Persona.Nombre == null)
                {
                    return ("El nombre no puede ser nulo", false);
                }
                if (encargado.Persona.PrimerApellido == "") { 
                    return ("El primer apellido no puede estar vacío", false);
                }
                if (encargado.Persona.SegundoApellido == "")
                {
                    return ("El segundo apellido no puede estar vacío", false);
                }
                if (encargado.Persona.Nombre == "")
                {
                    return ("El nombre no puede estar vacío", false);
                }
                if (encargado.Unidad.UnidadID == 0)
                {
                    return ("La unidad no puede estar vacía", false);
                }
                if (encargado.Unidad.UnidadID == null)
                {
                    return ("La unidad no puede ser nula", false);
                }
                
                if (!System.Text.RegularExpressions.Regex.IsMatch(encargado.Persona.Nombre, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$"))
                {
                    return ("El nombre contiene caracteres no permitidos", false);
                }
                if (!System.Text.RegularExpressions.Regex.IsMatch(encargado.Persona.PrimerApellido, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$"))
                {
                    return ("El primer apellido contiene caracteres no permitidos", false);
                }

                if (!System.Text.RegularExpressions.Regex.IsMatch(encargado.Persona.SegundoApellido, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$"))
                {
                    return ("El segundo apellido contiene caracteres no permitidos", false);
                }

            }
            return await encargadoAD.RegistrarEncargado(encargado);
        }
        public async Task<List<Encargado>> ListarEncargados()
        {
            List<Encargado> encargados = new List<Encargado>();
            encargados = await this.encargadoAD.ListarEncargados();
            return encargados;
        }

        public async Task<List<Encargado>> ListarEncargadosPorID(int? unidadID)
        {
            List<Encargado> encargadosFiltrados = new List<Encargado>();
            encargadosFiltrados = await encargadoAD.ListarEncargadosPorID(unidadID);
            return encargadosFiltrados;
        }

        public async Task<(string, bool)> ActualizarEncargado(Encargado encargado)
        {
            if (encargado != null)
            {
                if (encargado.UnidadID == 0)
                {
                    return ("La unidad es erronea", false);
                }
                if (encargado.Persona.SegundoApellido == null)
                {
                    return ("El segundo apellido no puede ser nulo", false);
                }
                if (encargado.Persona.PrimerApellido == null)
                {
                    return ("El primer apellido no puede ser nulo", false);
                }
                if (encargado.Persona.Nombre == null)
                {
                    return ("El nombre no puede ser nulo", false);
                }
                if (encargado.Persona.PrimerApellido == "")
                {
                    return ("El primer apellido no puede estar vacío", false);
                }
                if (encargado.Persona.SegundoApellido == "")
                {
                    return ("El segundo apellido no puede estar vacío", false);
                }
                if (encargado.Persona.Nombre == "")
                {
                    return ("El nombre no estar vacío", false);
                }
                if (!System.Text.RegularExpressions.Regex.IsMatch(encargado.Persona.Nombre, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$"))
                {
                    return ("El nombre contiene caracteres no permitidos", false);
                }
                if (!System.Text.RegularExpressions.Regex.IsMatch(encargado.Persona.PrimerApellido, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$"))
                {
                    return ("El primer apellido contiene caracteres no permitidos", false);
                }

                if (!System.Text.RegularExpressions.Regex.IsMatch(encargado.Persona.SegundoApellido, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$"))
                {
                    return ("El segundo apellido contiene caracteres no permitidos", false);
                }


            }
            return await encargadoAD.ActualizarEncargado(encargado);
        }
        public async Task<(string, bool)> EliminarEncargado(int encargadoID)
        {
            return await encargadoAD.EliminarEncargado(encargadoID);
        }
    }
}
