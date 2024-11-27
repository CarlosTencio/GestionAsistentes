using GestionAsistente.Entidades;
using GestionAsistente.ReglasNegocio;


namespace GestionAsistente.BlazorUI.Controlador
{
    public class BadgeControlador
    {

        private readonly BadgeRN badgeRN;

        public List<Badge> listaBadge = new List<Badge>();
        public BadgeControlador()
        {
            badgeRN = new BadgeRN();
        }
        public async Task<(bool, string)> RegistrarBadge(Badge badge)
        {
            return await badgeRN.RegistrarBadge(badge);
        }

        public async Task<List<Badge>> ListarBadge()
        {
            return await badgeRN.ListarBadge();
        }

        public async Task<List<Badge>> BuscarBadgesPorNumero(int numBadge)
        {
            return await badgeRN.BuscarBadgesPorNumero(numBadge);
        }

        public async Task<List<Badge>> BuscarBadgesPorNombreUnidad(string nombreUnidad)
        {
            return await badgeRN.BuscarBadgesPorNombreUnidad(nombreUnidad);
        }

        public async Task<bool> EliminarBadge(int badgeID)
        {
            return await badgeRN.EliminarBadge(badgeID);
        }

        public async Task<(bool, string)> ActualizarBadge(Badge badge)
        {
            return await badgeRN.ActualizarBadge(badge);
        }

        public async Task<List<Badge>> ListarBadgePorUnidad(int? unidadID)
        {
            return await badgeRN.ListarBadgePorUnidad(unidadID);
        }

        public async Task<Badge> ObtenerBadgePorId(int? badgeID)
        {
            return await badgeRN.ObtenerBadgePorId(badgeID);
        }
    }
}
