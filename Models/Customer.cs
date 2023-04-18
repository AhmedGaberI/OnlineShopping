using System.ComponentModel.DataAnnotations;

namespace OnlineShopping.Models
{
    public class Customer
    {
        [Required] public int CustomerId { get; set; }


        [Required] public string? CustomerName { get; set; }
        [Required] public string? CustomerEmail { get; set; }
        [Required] public string? CustomerCountry {get; set; } 

        public string? CustomerAddress { get; set;}
        public string? CustomerGender { get; set; }
        public string? JobTitle { get; set; }
        public string? CustomerPhone { get; set; }
        public string? Complaints { get; set;}
        
        
            
    }
}
