using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FirstApiMVC.DBContexts.Models;

[Table("PartnerType")]
public partial class PartnerType
{
    [Key]
    [Column("PartnerTypeID")]
    public int PartnerTypeId { get; set; }

    [StringLength(250)]
    public string? PartnerTypeName { get; set; }

    public bool? IsActive { get; set; }
}
