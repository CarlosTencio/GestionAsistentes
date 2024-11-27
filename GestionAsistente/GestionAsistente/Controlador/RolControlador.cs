using GestionAsistente.Entidades;
using GestionAsistente.ReglasNegocio;

namespace GestionAsistente.BlazorUI.Controlador
{
    public class RolControlador
    {

        private readonly RolRN rolRN;
        public RolControlador()
        {
            this.rolRN = new RolRN();
        }

        public async Task<List<Rol>> ListarRoles()
        {

            return await this.rolRN.ListarRoles();
        }

    }
}
