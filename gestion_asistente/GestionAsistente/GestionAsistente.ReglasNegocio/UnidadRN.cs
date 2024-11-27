using GestionAsistente.AccesoDatos.EntidadesAD;
using GestionAsistente.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GestionAsistente.ReglasNegocio
{
    public class UnidadRN
    {
        private readonly UnidadAD unidadAD;
        public UnidadRN() { 
            this.unidadAD = new UnidadAD();
        }
        public async Task<(bool exito, string mensaje)> RegistrarUnidad(Unidad unidad)
        {
            const string MENSAJE_EXITO = "Se registró con éxito";
            if (unidad == null)
            {
                throw new ArgumentNullException(nameof(unidad));
            }
            if (string.IsNullOrWhiteSpace(unidad.Nombre))
            {
                return (false, "El nombre de la unidad es obligatorio.");
            }
            // Nombres duplicados
            var unidadesExistentes = await unidadAD.listarUnidades();
            //bool existeNombreRepetido = unidadesExistentes.Any(u => u.Nombre.ToLower() == unidad.Nombre.ToLower());
            bool existeNombreRepetido = unidadesExistentes.Any(u => u.Nombre.Equals(unidad.Nombre, StringComparison.OrdinalIgnoreCase));
            if (existeNombreRepetido)
            {
                return (false, "Ya existe una unidad con este nombre.");
            }
            if (int.TryParse(unidad.Nombre, out _))
            {
                return (false, "El nombre de la unidad no puede ser solo numérico.");
            }
            if (!Regex.IsMatch(unidad.Nombre, @"^[a-zA-Z0-9áéíóúÁÉÍÓÚ\s]+$"))
            {
                return (false, "El nombre de la unidad solo puede contener letras y números.");
            }
            
            var registroExitoso = await unidadAD.RegistrarUnidad(unidad);
            return (registroExitoso, MENSAJE_EXITO); //await unidadAD.RegistrarUnidad(unidad);
        }
        public async Task<List<Unidad>> ListarUnidades()
        {
            List<Unidad> unidades = new List<Unidad>();
            unidades = await this.unidadAD.listarUnidades();
            return unidades;
        }
        public async Task<(string, bool)> ActualizarUnidad(Unidad unidad)
        {
            if (unidad != null)
            {
                if (unidad.UnidadID == 0)
                {
                    return ("La unidad es erronea", false);
                }
                if (string.IsNullOrWhiteSpace(unidad.Nombre))
                {
                    return ("El nombre de la unidad es necesario", false);
                }
                // Nombres duplicados
                var unidadesExistentes = await unidadAD.listarUnidades();
                //bool existeNombreRepetido = unidadesExistentes.Any(u => u.Nombre.ToLower() == unidad.Nombre.ToLower());
                bool existeNombreRepetido = unidadesExistentes.Any(u => u.Nombre.Equals(unidad.Nombre, StringComparison.OrdinalIgnoreCase));
                if (existeNombreRepetido)
                {
                    return ("Ya existe una unidad con este nombre.", false);
                }
                if (!Regex.IsMatch(unidad.Nombre, @"^[a-zA-Z0-9áéíóúÁÉÍÓÚ\s]+$"))
                {
                    return ("El nombre de la unidad solo puede contener letras y números.", false);
                }
            }
            return await unidadAD.ActualizarUnidad(unidad);
        }
        public async Task<(string, bool)> EliminarUnidad(int unidadID)
        {
            return await unidadAD.EliminarUnidad(unidadID);
        }
    }
}
