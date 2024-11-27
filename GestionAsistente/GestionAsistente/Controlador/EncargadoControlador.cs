using GestionAsistente.Entidades;
using GestionAsistente.ReglasNegocio;

namespace GestionAsistente.BlazorUI.Controlador
{
    public class EncargadoControlador
    {
        private readonly EncargadoRN encargadoRN;
        public UnidadControlador unidadControlador = new UnidadControlador();
        public List<Unidad> listaUnidades = new List<Unidad>();
        public List<Encargado> listaEncargados = new List<Encargado>();
        public string mensajeExito;
        public string mensajeError;
        public int? unidad;
        public Encargado encargado = new Encargado
        {
            Persona = new Persona(),// Inicializa la persona dentro del encargado
            Unidad = new Unidad()// Inicializa la unidad dentro del encargado
        };
        public Encargado crearEncargado = new Encargado
        {
            Persona = new Persona(),
            Unidad = new Unidad()
        };

        public Encargado eliminarEncargado = new Encargado
        {
            Persona = new Persona(),
            Unidad = new Unidad()
        };

        public EncargadoControlador()
        {
            encargadoRN = new EncargadoRN();
        }
        
        public Encargado actaulizarEncargado = new Encargado
        {
            Persona = new Persona()
        };
        public async Task EditarEncargado(int encargadoID)

        {
            actaulizarEncargado = listaEncargados.Find(x => x.EncargadoID == encargadoID);

            encargado.EncargadoID = actaulizarEncargado.EncargadoID;
            encargado.PersonaID = actaulizarEncargado.PersonaID;
            encargado.Persona.Nombre = actaulizarEncargado.Persona.Nombre;
            encargado.Persona.PrimerApellido = actaulizarEncargado.Persona.PrimerApellido;
            encargado.Persona.SegundoApellido = actaulizarEncargado.Persona.SegundoApellido;
            encargado.UnidadID = actaulizarEncargado.UnidadID;
            encargado.Unidad.Nombre = actaulizarEncargado.Unidad.Nombre;
            encargado.Unidad.UnidadID = actaulizarEncargado.Unidad.UnidadID;

        }
        public async Task seleccionarEncargadoEliminar(int eliminarEncargadoS)

        {

            eliminarEncargado = listaEncargados.Find(x => x.EncargadoID == eliminarEncargadoS);

        }
        public async Task listarEncargados()
        {
            if(unidad == null)
                this.listaEncargados = await encargadoRN.ListarEncargados();
            else if(unidad != null)
                this.listaEncargados = await encargadoRN.ListarEncargadosPorID(unidad);
        }
        public async Task<List<Encargado>> listarEncargadosPaginados(int currentPage, int pageSize)
        {
            List<Encargado> allEncargados;

            if (unidad == null)
            {
                return allEncargados = await encargadoRN.ListarEncargados(); 
            }
            else
            {
                return allEncargados = await encargadoRN.ListarEncargadosPorID(unidad); 
            }
        }

        public async Task listarUnidades()
        {
         this.listaUnidades= await unidadControlador.ListarUnidades();
            if (unidad != null)
            {
                this.listaUnidades= listaUnidades.Where(x => x.UnidadID == unidad).ToList();
            }

        }
        public async Task<(string, bool)> registrarEncargado(Encargado encargado) { 
            return await encargadoRN.RegistrarEncardado(encargado);
        }
        public async Task<(string, bool)> RegistrarEncargado(Encargado encargado)
        {
            (string mensaje, bool esExitoso) exito = ("", false);
            try
            {
                exito = await encargadoRN.RegistrarEncardado(encargado);

                if (exito.esExitoso)
                {
                    mensajeExito = "Encargado registrado con éxito.";
                    mensajeError = null;
                   
                }
                else
                {
                    mensajeExito = null;
                    mensajeError = "Hubo un problema al registrar el encargado.";
                }
            }
            catch (Exception ex)
            {
                mensajeError = $"Error: {ex.Message}";
                mensajeExito = null;
            }
            return exito;

        }

        public async Task<List<Encargado>> ListarEncargados()
        {
            return await encargadoRN.ListarEncargados();
        }

        public async Task<List<Encargado>> ListarEncargadosPorID(int? unidadID)
        {
            return await encargadoRN.ListarEncargadosPorID(unidadID);
        }
        public async Task<(string, bool)> ActualizarEncargado(Encargado encargado)
        {
            return await encargadoRN.ActualizarEncargado(encargado);
        }
        public async Task<(string, bool)> EliminarEncargado(int encargadoID)
        {
            return await encargadoRN.EliminarEncargado(encargadoID);
        }
    }
}
