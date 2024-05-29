using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FirstApiMVC.DBContexts.Models;

public partial class Sale
{
    [Key]
    public int SalesId { get; set; }

    public int? CustomerId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? SalesDate { get; set; }

    public bool? IsActive { get; set; }
}
