using System.ComponentModel.DataAnnotations;

namespace FirstApiMVC.DTO
{
    public class PartnerDto
    {
       
        public string? PartnerTypeName { get; set; }
        public string? PartnerName { get; set; }
        public bool? IsActive { get; set; }
        
    }
}
