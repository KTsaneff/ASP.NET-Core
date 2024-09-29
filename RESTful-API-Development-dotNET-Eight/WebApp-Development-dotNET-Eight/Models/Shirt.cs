using WebApp_Development_dotNET_Eight.Models.Validations;
using System.ComponentModel.DataAnnotations;

namespace WebApp_Development_dotNET_Eight.Models
{
    public class Shirt
    {
        public int ShirtId { get; set; }

        [Required]
        public string Brand { get; set; } = null!;

        [Required]
        public string Color { get; set; } = null!;

        [Shirt_EnsureCorrectSizing]
        public int? Size { get; set; }

        [Required]
        public string Gender { get; set; } = null!;

        public double? Price { get; set; }
    }
}
