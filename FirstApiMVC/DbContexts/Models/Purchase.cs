using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FirstApiMVC.DBContexts.Models;

[Table("Purchase")]
public partial class Purchase
{
    [Key]
    [Column("PurchaseID")]
    public int PurchaseId { get; set; }

    public int? SupplierId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? PurchaseDate { get; set; }

    public bool? IsActive { get; set; }
}
