using System.ComponentModel.DataAnnotations.Schema;

namespace EndProject.Backend.Models
{
    public class Ingredienten
    {
        public int id { get; set; }
        public string naam { get; set; }
        public string hoeveelheid { get;set; }
        public string? eenheid { get;set; }
        public DateTime? verwijderd { get; set; } 

        [ForeignKey("Recept")]
        public int recept_id { get; set; }

    }
}
