using System.ComponentModel.DataAnnotations;

namespace FirstApiMVC.DTO
{
    public class PurchaseDetailDto
    {
        [Required]
        public int ItemId { get; set; }
        public int ItemQuantity { get; set; }
        public decimal UnitPrice { get; set; }
       
    }
    
}
