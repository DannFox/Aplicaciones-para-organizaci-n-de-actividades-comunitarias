namespace GestorActividades.Models
{
    public class Participante
    {
        public int ParticipanteId { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public ICollection<Actividad> Actividades    { get; set; } = new List<Actividad>();
    }
}
