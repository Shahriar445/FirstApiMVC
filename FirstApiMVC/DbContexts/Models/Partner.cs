using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FirstApiMVC.DBContexts.Models;

[Table("Partner")]
public partial class Partner
{
    [Key]
    public int PartnerId { get; set; }

    [StringLength(250)]
    public string? PartnerName { get; set; }

    public int? PartnerTypeId { get; set; }

    public bool? IsActive { get; set; }
}
