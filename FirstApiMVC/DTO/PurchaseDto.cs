using Microsoft.VisualBasic;

namespace FirstApiMVC.DTO
{
    public class PurchaseDto
    {
        public int SupplierId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public bool IsActive { get; set; }
        public List<PurchaseDetailDto>? PurchaseDetails { get; set; }


    }
}
