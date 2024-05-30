using System.ComponentModel.DataAnnotations.Schema;

namespace FirstApiMVC.DTO
{
    public class SalesDetailsDto
    {

        public int? ItemId { get; set; }

        public int? ItemQuantity { get; set; }

        public decimal? UnitPrice { get; set; }

    }
}
