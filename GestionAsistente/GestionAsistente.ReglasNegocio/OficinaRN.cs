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
    public class OficinaRN
    {
        private readonly OficinaAD oficinaAD;
        public OficinaRN()
        {
            oficinaAD = new OficinaAD();
        }
        public async Task<List<Oficina>> ListarOficinas()
        {
            List<OficinaEF> oficinasEF = this.oficinaAD.ObtenerOficinas();
            List<Oficina> oficinas = new List<Oficina>();
            foreach (var oficinaEF in oficinasEF)
            {
                Oficina oficina = new Oficina
                {
                    Nombre = oficinaEF.Nombre,
                    OficinaID = oficinaEF.OficinaID
                };
                oficinas.Add(oficina);
            }
            return oficinas;
        }

        public async Task<int> RegistrarOficina(Oficina oficina)
        {
            if (oficina == null)
            {
                throw new ArgumentNullException(nameof(oficina));
            }
            return await oficinaAD.RegistrarOficina(oficina); // Sin await si no es asíncrono
        }

        public async Task<bool> EliminarOficina(int oficinaID)
        {
            if (oficinaID == null)
            {
                throw new ArgumentNullException(nameof(oficinaID));
            }
            return await oficinaAD.EliminarOficina(oficinaID); // Sin await si no es asíncrono
        }

        public async Task<bool> ExisteOficina(string nombre)
        {
            if (nombre == null)
            {
                throw new ArgumentNullException(nameof(nombre));
            }
            return await oficinaAD.ExisteOficina(nombre); // Sin await si no es asíncrono
        }

        public async Task<(string, bool)> ActualizarOficina(Oficina oficina, int cantidadEstaciones)
        {
            if (oficina != null)
            {
                if (oficina.OficinaID == 0)
                {
                    return ("La unidad es erronea", false);
                }
            }
            return await oficinaAD.ModificarOficina(oficina, cantidadEstaciones);
        }


    }
}
