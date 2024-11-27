using GestionAsistente.Entidades;
using GestionAsistente.ReglasNegocio;

namespace GestionAsistente.BlazorUI.Controlador
{
    public class UsuarioControlador
    {

        private readonly UsuarioRN usuarioRN;

        public UsuarioControlador()
        {
            usuarioRN = new UsuarioRN();
        }
        public async Task<Usuario> VerificarContrasenia(Usuario usuario)
        {
            return await usuarioRN.VerificarContrasenia(usuario);
        }
            public async Task<(string, bool)> RegistrarUsuario(Usuario usuario)
        {
            return await usuarioRN.RegistrarUsuario(usuario);
        }

        public async Task<List<Usuario>> ListarUsuarios()
        {
            return await usuarioRN.ListarUsuarios();
        }

        public async Task<(string, bool)> ActualizarUsuario(Usuario usuario)
        {
            return await usuarioRN.ActualizarUsuario(usuario);
        }
        public async Task<(string, bool)> EliminarUsuario(int usuarioID)
        {
            return await usuarioRN.EliminarUsuario(usuarioID);
        }
        //Nuevo
        public async Task<int> ContarUsuarios()
        {
            return await usuarioRN.ContarUsuarios();
        }
        public async Task<List<Usuario>> ObtenerUsuariosPaginados(int currentPage, int pageSize)
        {
            return await usuarioRN.ObtenerUsuariosPaginados(currentPage, pageSize);
        }
    }
}
