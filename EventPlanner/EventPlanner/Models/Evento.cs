namespace EventPlanner.Models
{
    public class Evento
    {
        public int Id { get; set; }

        public string Titulo { get; set; } = null!;

        public DateTime Fecha { get; set; }

        public string Descripcion { get; set; } = null!;

        public string Ubicacion { get; set;} = null!;

    }
}
