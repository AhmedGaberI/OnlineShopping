using System.ComponentModel.DataAnnotations;


namespace OnlineShopping.Models
{
    public class Product
    {
      
        public int ProductId { get; set; }
        [Required] public string ProductName { get; set;}
        [Required] public string SKU { get; set; }
        [Required] public int CompanyId { get; set; }
        [Required] public int Qty { get; set; }
        [Required] public int Pprice { get; set; }
        [Required] public int Sprice { get; set; }
        [Required] public int ModelId {get ; set; }
        [Required] public int CategoryId { get; set; }
        [Required] public int BrandId { get; set; }  
        

        public string? Description { get; set; }
        public string? Color { get; set; }
        public string? ExpiredDate { get; set; }
        public int Size { get; set; }

        
    }
}
