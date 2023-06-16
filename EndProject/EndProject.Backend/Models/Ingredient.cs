using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EndProject.Backend.Models
{
    public class Ingredient
    {
        [Key]
        public int Id { get; set; }
        public string Naam { get; set; }
        public string Hoeveelheid { get;set; }
        public string? Eenheid { get;set; }
        public DateTime? Verwijderd { get; set; } 

        public bool? Kopen { get; set; }

        [ForeignKey("Recept")]
        public int Receptid { get; set; }

    }
}
