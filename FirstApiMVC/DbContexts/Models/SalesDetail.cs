using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FirstApiMVC.DBContexts.Models;

public partial class SalesDetail
{
    [Key]
    public int DetailsId { get; set; }

    public int? SalesId { get; set; }

    public int? ItemId { get; set; }

    public int? ItemQuantity { get; set; }

    [Column(TypeName = "decimal(18, 0)")]
    public decimal? UnitPrice { get; set; }

    public bool? IsActive { get; set; }
}
