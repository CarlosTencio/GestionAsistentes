using GestionAsistente.AccesoDatos.EntidadesAD;
using GestionAsistente.Entidades;

namespace GestionAsistente.ReglasNegocio
{
    public class AsistenteRN
    {
        private readonly AsistenteAD asistenteAD;
        public AsistenteRN()
        {
            asistenteAD = new AsistenteAD();
        }

        private class ValidacionResultado
        {
            public bool EsValido { get; set; }
            public string Mensaje { get; }

            public ValidacionResultado(bool esValido, string mensaje)
            {
                EsValido = esValido;
                Mensaje = mensaje;
            }
        }

        public async Task<(bool exito, string mensaje)> RegistrarAsistente(Asistente asistente)
        {
            const string MENSAJE_EXITO = "Se registró con éxito";
            if (asistente == null)
            {
                return (false, "Llene los campos para poder registrar.");
            }
            var validacion = ValidarAsistente(asistente);
            if (!validacion.EsValido)
            {
                return (false, validacion.Mensaje);
            }
            var registroExitoso = await asistenteAD.RegistrarAsistente(asistente);
            if (!registroExitoso)
            {
                string mensajeFallo = "No se pudo registrar";
                return (registroExitoso, mensajeFallo);
            }
            return (registroExitoso, MENSAJE_EXITO);
        }

        private ValidacionResultado ValidarAsistente(Asistente asistente)
        {
            // Expresión regular que solo permite letras y espacios (para nombres)
            const string PATRON_SOLO_LETRAS = @"^[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ\s]+$";
            // Expresión regular que detecta caracteres especiales no permitidos
            const string PATRON_CARACTERES_ESPECIALES = @"[<>\(\)\[\]]";
            // Expresión regular que permite letras y números (para usuario, contraseña y accesos)
            const string PATRON_LETRAS_NUMEROS = @"^[a-zA-Z0-9]+$";

            var reglasDeNegocio = new Dictionary<Func<Asistente, bool>, string>
    {
        // Validaciones del nombre de usuario
        { a => !string.IsNullOrWhiteSpace(a.nombreUsuario),
          "El nombre de usuario no puede estar en blanco" },
        { a => System.Text.RegularExpressions.Regex.IsMatch(a.nombreUsuario, PATRON_LETRAS_NUMEROS),
          "El nombre de usuario solo puede contener letras y números" },
        { a => !System.Text.RegularExpressions.Regex.IsMatch(a.nombreUsuario, PATRON_CARACTERES_ESPECIALES),
          "El nombre de usuario no debe contener los caracteres <, >, (), []" },
        
                // Validaciones de la Persona
        { a => a.Persona != null,
          "Los datos personales son requeridos" },
        
        // Validaciones de nombre
        { a => a.Persona != null && !string.IsNullOrWhiteSpace(a.Persona.Nombre),
          "El nombre no puede estar en blanco" },
        { a => a.Persona != null &&
               System.Text.RegularExpressions.Regex.IsMatch(a.Persona.Nombre, PATRON_SOLO_LETRAS),
          "El nombre solo debe contener letras" },
        { a => a.Persona != null &&
               !System.Text.RegularExpressions.Regex.IsMatch(a.Persona.Nombre, PATRON_CARACTERES_ESPECIALES),
          "El nombre no debe contener los caracteres <, >, (), []" },
        
        // Validaciones de primer apellido
        { a => a.Persona != null && !string.IsNullOrWhiteSpace(a.Persona.PrimerApellido),
          "El primer apellido no puede estar en blanco" },
        { a => a.Persona != null &&
               System.Text.RegularExpressions.Regex.IsMatch(a.Persona.PrimerApellido, PATRON_SOLO_LETRAS),
          "El primer apellido solo debe contener letras" },
        { a => a.Persona != null &&
               !System.Text.RegularExpressions.Regex.IsMatch(a.Persona.PrimerApellido, PATRON_CARACTERES_ESPECIALES),
          "El primer apellido no debe contener los caracteres <, >, (), []" },
        
        // Validaciones de segundo apellido
        { a => a.Persona != null && !string.IsNullOrWhiteSpace(a.Persona.SegundoApellido),
          "El segundo apellido no puede estar en blanco" },
        { a => a.Persona != null &&
               System.Text.RegularExpressions.Regex.IsMatch(a.Persona.SegundoApellido, PATRON_SOLO_LETRAS),
          "El segundo apellido solo debe contener letras" },
        { a => a.Persona != null &&
               !System.Text.RegularExpressions.Regex.IsMatch(a.Persona.SegundoApellido, PATRON_CARACTERES_ESPECIALES),
          "El segundo apellido no debe contener los caracteres <, >, (), []" },
        // Validaciones de accesos
        { a => a.Accesos != null,
          "Los accesos no pueden estar en blanco" },
        // { a => a.Accesos != null && System.Text.RegularExpressions.Regex.IsMatch(a.Accesos, PATRON_LETRAS_NUMEROS),
        //   "Los accesos solo pueden contener letras y números" },
        { a => a.Accesos != null && !System.Text.RegularExpressions.Regex.IsMatch(a.Accesos, PATRON_CARACTERES_ESPECIALES),
          "Los accesos no deben contener los caracteres <, >, (), []" },

          // Validaciones de la contraseña
        { a => !string.IsNullOrWhiteSpace(a.Contrasenia),
          "La contraseña no puede estar en blanco" },
        { a => System.Text.RegularExpressions.Regex.IsMatch(a.Contrasenia, PATRON_LETRAS_NUMEROS),
          "La contraseña solo puede contener letras y números" },
        { a => !System.Text.RegularExpressions.Regex.IsMatch(a.Contrasenia, PATRON_CARACTERES_ESPECIALES),
          "La contraseña no debe contener los caracteres <, >, (), []" },
    };

            foreach (var regla in reglasDeNegocio)
            {
                if (!regla.Key(asistente))
                {
                    return new ValidacionResultado(false, regla.Value);
                }
            }
            return new ValidacionResultado(true, string.Empty);
        }
        public async Task<List<Asistente>> ListarAsistentes()
        {
            List<Asistente> asistentes = new List<Asistente>();
            asistentes = await this.asistenteAD.listarAsistentes();
            return asistentes;
        }

        public async Task<List<Asistente>> ListarAsistentesPorUnidadID(int? unidad)
        {
            List<Asistente> asistentes = new List<Asistente>();
            asistentes = await this.asistenteAD.ListarAsistentesPorUnidadID(unidad);
            return asistentes;
        }
        public async Task<List<Asistente>> BuscarAsistentePorNombre(string nombre)
        {
            List<Asistente> asistentes = new List<Asistente>();
            asistentes = await this.asistenteAD.BuscarAsistentePorNombre(nombre);
            return asistentes;
        }

        public async Task<(bool, string)> EliminarAsistente(int? asistenteID)
        {
            string mensaje;
            bool confirmarEliminacion = await asistenteAD.EliminarAsistente(asistenteID);
            if (confirmarEliminacion)
            {
                mensaje = "Se eliminó con éxito";
            }
            else
            {
                mensaje = "No se pudo eliminar";
            }
            return (confirmarEliminacion, mensaje);
        }

        public async Task<(bool exito, string mensaje)> ActualizarAsistente(Asistente asistente)
        {

            const string MENSAJE_EXITO = "Se actualizó con éxito";
            if (asistente == null)
            {
                return (false, "Modifique los campos para poder actualizar.");
            }
            var validacion = ValidarAsistente(asistente);
            if (!validacion.EsValido)
            {
                return (false, validacion.Mensaje);
            }
            var actualizacionExitosa = await asistenteAD.ModificarAsistente(asistente);

            if (!actualizacionExitosa)
            {
                string mensajeFallo = "No se pudo registrar";
                return (actualizacionExitosa, mensajeFallo);
            }
            return (actualizacionExitosa, MENSAJE_EXITO);
        }

    }
}
