using System.ComponentModel.DataAnnotations;

namespace OnlineShopping.Models
{
    public class Brand
    {
        [Required]
        public int BrandId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
