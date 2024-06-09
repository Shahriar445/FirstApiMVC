using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FirstApiMVC.DTO
{
    public class ItemListDto
    {
        public List<ItemDto> Items { get; set; }
      
    }
    public class ItemDto
    {
        
        public int ItemId { get; set; }

        
        public string? ItemName { get; set; }

        public int? NumStockQuantity { get; set; }

        public bool? IsActive { get; set; }
    
        public string? ImageUrl { get; set; }
       
    }

}
