using GestionAsistente.Entidades;
using GestionAsistentes.ReglasNegocio;
using Microsoft.AspNetCore.Mvc;

namespace GestionAsistente.BlazorUI.Controlador
{
    public class PersonaControlador : Controller
    {
        private readonly PersonaRN personaRN;
        public PersonaControlador()
        {
            personaRN = new PersonaRN();
        }
        public void guardarPersona(Persona persona)
        {
            personaRN.guardarPersona(persona);
        }
    }
}
