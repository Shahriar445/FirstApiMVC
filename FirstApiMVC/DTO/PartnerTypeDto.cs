using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstApiMVC.DTO
{
    public class PartnerTypeDto
    {
        
      
        public int PartnerTypeId { get; set; }

       
        public string? PartnerTypeName { get; set; }

        public bool? IsActive { get; set; }
    }
}
