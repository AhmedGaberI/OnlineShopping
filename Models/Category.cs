using System.ComponentModel.DataAnnotations;

namespace OnlineShopping.Models
{
    public class Category
    {
        [Required]
        public int CategoryId { get; set; }

        [Required]
        public string Name { get; set; }
        
    }
}
