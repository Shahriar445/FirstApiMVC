using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FirstApiMVC.DBContexts.Models;

[Table("Item")]
public partial class Item
{
    [Key]
    public int ItemId { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string? ItemName { get; set; }

    public int? NumStockQuantity { get; set; }

    public bool? IsActive { get; set; }
}
