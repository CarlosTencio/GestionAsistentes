using GestionAsistente.Entidades;
using GestionAsistente.ReglasNegocio;



namespace GestionAsistente.BlazorUI.Controlador
{
    public class HorarioControlador
    {
        private readonly HorarioRN horarioRN;
        private List<Horario> horarios;
        public List<Horario> renderizarHorario;
        public List<EstacionTrabajo> horariosAgrupados;
        public List<Horario> horarioTemporal = new List<Horario>();
        public List<string> horasFijas = new List<string>
            {
                "07:00", "08:00", "09:00", "10:00", "11:00", "12:00",
                "13:00", "14:00", "15:00", "16:00", "17:00", "18:00"
            };
        public int estacionTrabajoIDEliminar;
        public HorarioControlador()
        {
            horarioRN = new HorarioRN();
        }
        public async Task<bool> RegistrarHorario(Horario horario)
        {
            return await horarioRN.RegistrarHorario(horario);
        }
        public async Task<List<Horario>> ListarHorariosPorEstacionTrabajo(int estacionTrabajoID, string dia)
        {
            this.horarios = await horarioRN.ListarHorariosPorEstacionTrabajo(estacionTrabajoID);
            editarHorasDisponibles(dia);
            return await horarioRN.ListarHorariosPorEstacionTrabajo(estacionTrabajoID);
        }
        // Lista de días de la semana
        public List<string> diasDeLaSemana = new List<string>
                {
                "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo"
                };

        // Lista de horas (de 7 a 20)
        public List<string> horasDelDiaInicio = new();
        public List<string> horasDelDiaFinal = new();
        public async Task editarHorasDisponibles(string dia)
        {
            var todasLasHoras = new List<string>
            {
                "07:00", "08:00", "09:00", "10:00", "11:00", "12:00",
                "13:00", "14:00", "15:00", "16:00", "17:00", "18:00"
            };

            // Inicializa las listas de horas disponibles
            this.horasDelDiaInicio = new List<string>(todasLasHoras);
            this.horasDelDiaFinal = new List<string>(todasLasHoras);
            Console.WriteLine(dia);
            // Obtiene las horas ocupadas
            var horasOcupadas = horarios
                .Where(h => h.Dia == dia)
                .Select(h => new
                {
                    Inicio = h.HoraInicio.TimeOfDay,
                    Fin = h.HoraFin.TimeOfDay
                })
                .ToList();

            // Crea un conjunto de horas no disponibles
            var horasNoDisponibles = new HashSet<string>();

            foreach (var ocupado in horasOcupadas)
            {
                // Itera a través de todas las horas y marca las horas ocupadas
                foreach (var hora in todasLasHoras)
                {
                    var horaActual = TimeSpan.Parse(hora);
                    if (horaActual >= ocupado.Inicio && horaActual <= ocupado.Fin)
                    {
                        horasNoDisponibles.Add(hora);
                    }
                }
            }
            // Filtra las horas del día de inicio y final
            horasDelDiaInicio = todasLasHoras.Except(horasNoDisponibles).ToList();
            horasDelDiaFinal = todasLasHoras.Except(horasNoDisponibles).ToList();
        }
        public async Task verificarHoraSeleccionar(string horaInicio)
        {
            horasDelDiaFinal = horasDelDiaInicio;
            // Convierte la hora de inicio a un formato `TimeSpan` para comparación
            TimeSpan horaInicioTimeSpan = TimeSpan.Parse(horaInicio);

            // Filtra las horas del día final para eliminar todas las horas menores o iguales a la hora de inicio
            horasDelDiaFinal = horasDelDiaFinal
                .Where(h => TimeSpan.Parse(h) > horaInicioTimeSpan)
                .ToList();
        }

        public async Task renderizarHorariosPorOficina(int oficinaID)
        {
            renderizarHorario = await horarioRN.ListarHorariosPorOficina(oficinaID);
            await ListarHorariosPorOficinaAgrupados(oficinaID);
        }
        public async Task ListarHorariosPorOficinaAgrupados(int oficinaID)
        {

            horariosAgrupados = new List<EstacionTrabajo>(); // Inicializa la lista
            renderizarHorario = await horarioRN.ListarHorariosPorOficina(oficinaID);

            foreach (var horario in renderizarHorario)
            {
                // Verifica si el EstacionTrabajo ya existe en la lista agrupada
                if (!horariosAgrupados.Any(h => h.EstacionTrabajoID == horario.EstacionTrabajo.EstacionTrabajoID)) // Asegúrate de comparar correctamente
                {
                    horariosAgrupados.Add(horario.EstacionTrabajo);
                }
            }
        }
        public async Task<bool> limpiarHararioEstacion(int estacionTrabajoID)
        {
            return await horarioRN.limpiarHararioEstacion(estacionTrabajoID);

        }
        public async Task<bool> limpiarHorarioAsistente(int asistenteID)
        {
            return await horarioRN.limpiarHorarioAsistente(asistenteID);
        }
        public async Task<bool> limpiarHararioOficina(int asistenteID)
        {
            return await horarioRN.limpiarHararioOficina(asistenteID);
        }

    }

}
