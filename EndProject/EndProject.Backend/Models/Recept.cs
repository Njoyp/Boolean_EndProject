using System.ComponentModel.DataAnnotations;

namespace EndProject.Backend.Models
{
    public class Recept
    {
        [Key]
        public int Receptid { get; set; }
        public string Naam { get; set; }
        public int Tijd_min { get;set; }
        public string Stappen { get; set; }
        public DateTime? Verwijderd { get; set; }

        public IEnumerable<Ingredient> ingredienten { get; set; }

    }
}
