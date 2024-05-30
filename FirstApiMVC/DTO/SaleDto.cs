using System.ComponentModel.DataAnnotations.Schema;

namespace FirstApiMVC.DTO
{
    public class SaleDto
    {
    
        public int? CustomerId { get; set; }

        public DateTime? SalesDate { get; set; }

        public bool? IsActive { get; set; }
        public List<SalesDetailsDto> SaleDeatils{ get; set; }

    }
}
