namespace GestorActividades.Models
{
    public class Actividad
    {
        public int ActividadID { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public string Ubicacion { get; set; }

        public int OrganizadorID { get; set; }
        public Organizador Organizador { get; set; }

        public ICollection<Participante> Participantes { get; set; } = new List<Participante>();

        public string FechaFormateada => Fecha.ToString("d");
    }
}
