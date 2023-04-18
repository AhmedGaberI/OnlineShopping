using System.ComponentModel.DataAnnotations;

namespace OnlineShopping.Models
{
    public class Model
    {
        [Required]
        public int ModelId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
