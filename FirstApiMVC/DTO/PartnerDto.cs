using System.ComponentModel.DataAnnotations;

namespace FirstApiMVC.DTO
{
    public class PartnerDto
    {
        public int PartnerId { get; set; }
        public string? PartnerTypeName { get; set; }
        public string? PartnerName { get; set; }
        public bool? IsActive { get; set; }
        public int PartnerTypeId { get; set; }
    }
}
