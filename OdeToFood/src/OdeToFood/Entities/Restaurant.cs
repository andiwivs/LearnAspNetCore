using System.ComponentModel.DataAnnotations;

namespace OdeToFood.Entities
{

    public enum CuisineType
    {
        None,
        British,
        Chinese,
        Indian
    }

    public class Restaurant
    {
        public int Id { get; set; }

        [Required, MaxLength(80)]
        [Display(Name = "Restaurant Name")]
        public string Name { get; set; }

        [Required]
        public CuisineType Cuisine { get; set; }
    }
}
