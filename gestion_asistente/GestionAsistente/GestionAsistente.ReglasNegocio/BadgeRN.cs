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
    public class BadgeRN
    {
        private readonly BadgeAD badgeAD;

        public BadgeRN()
        {
            badgeAD = new BadgeAD();
        }

        public async Task<(bool, string)> RegistrarBadge(Badge badge)
        {
            string simboloSeparadorHorario = "a";

            bool exito = false;

            if (badge == null)
            {
                return (false, "Datos invalidos");
            }

            //si los datos son invalidos,
            //por ejemplo que siga el formato horaInicio a HoraFinal
            if(badge.Horario.Split(simboloSeparadorHorario).Length < 2)
            {
                return (false, "Datos invalidos");
            }

            //validar nulos
            if(string.IsNullOrEmpty(badge.Horario))
            {
                return (false, "Datos nulos ingresados");
            }

            exito = await badgeAD.RegistrarBadge(badge); // Sin await si no es asíncrono

            if (exito)
            {
                return (exito, "Registrado correctamente");
            }
            else
            {
                return (exito, "Error al registrar el badge");
            }
        }

        public async Task<List<Badge>> ListarBadge()
        {
            List<Badge> encargados = new List<Badge>();
            encargados = await this.badgeAD.ListarBadge();
            return encargados;
        }

        public async Task<List<Badge>> BuscarBadgesPorNumero(int numBadge)
        {
            return await badgeAD.BuscarBadgesPorNumero(numBadge);
        }

        public async Task<List<Badge>> BuscarBadgesPorNombreUnidad(string nombreUnidad)
        {
            return await badgeAD.BuscarBadgesPorNombreUnidad(nombreUnidad);
        }

        public async Task<bool> EliminarBadge(int badgeID)
        {
            if (badgeID == null)
            {
                throw new ArgumentNullException(nameof(badgeID));
            }
            return await badgeAD.EliminarBadge(badgeID);
        }

        public async Task<(bool, string)> ActualizarBadge(Badge badge)
        {
            string simboloSeparadorHorario = "a";

            bool exito = false;

            if (badge == null)
            {
                throw new ArgumentNullException(nameof(badge));
            }

            //si los datos son invalidos,
            //por ejemplo que siga el formato horaInicio a HoraFinal
            if (badge.Horario.Split(simboloSeparadorHorario).Length < 2)
            {
                return (false, "Datos invalidos");
            }

            //validar nulos
            if (string.IsNullOrEmpty(badge.Horario))
            {
                return (false, "Datos nulos ingresados");
            }

            exito = await badgeAD.ModificarBadge(badge);

            if (exito)
            {
                return (exito, "Actualizado correctamente");
            }
            else
            {
                return (exito, "Error al actualizar el badge");
            }
            
        }
        public async Task<List<Badge>> ListarBadgePorUnidad(int? unidadID)
        {
            if (unidadID == null)
            {
                return new List<Badge>();
            }
            return await badgeAD.listarBadgePorUnidad(unidadID);
        }
        public async Task<Badge> ObtenerBadgePorId(int? badgeID)
        {
            return await badgeAD.ObtenerBadgePorId(badgeID);
        }

    }
}
