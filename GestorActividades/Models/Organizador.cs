namespace GestorActividades.Models
{
    public class Organizador
    {
        public int OrganizadorID { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }

        public ICollection<Actividad> Actividades { get; set; }
    }
}
