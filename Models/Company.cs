using System.ComponentModel.DataAnnotations;

namespace OnlineShopping.Models
{
    public class Company
    {
        public int CompanyId { get; set; }

        [Required]
        public string CompanyName { get; set;}

        [Required]
        public string ComanyEmail { get; set;}

        public string? CompanyCountry { get; set; }

        public string? CompanyPhone { get; set;}
       
        public string? Website { get; set;}
    

        public string? Location { get; set;}
        public string? CompanyDescription { get; set; }
        
        public string? Industry { get; set; }


    }
}
