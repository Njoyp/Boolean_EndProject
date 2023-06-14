namespace EndProject.Backend.Models
{
    public class Recepten
    {
        public int id { get; set; }
        public string naam { get; set; }
        public int tijd_min { get;set; }
        public string stappen { get; set; }
        public DateTime? verwijderd { get; set; }

        public IEnumerable<Ingredienten> ingredienten { get; set; }

    }
}
