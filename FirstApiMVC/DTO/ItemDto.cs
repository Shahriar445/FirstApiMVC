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
        [Key]
        public int ItemId { get; set; }

        [StringLength(250)]
        [Unicode(false)]
        public string? ItemName { get; set; }

        public int? NumStockQuantity { get; set; }

        public bool? IsActive { get; set; }
    }
}
