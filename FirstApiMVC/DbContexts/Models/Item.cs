using System;
using System.Collections.Generic;

namespace FirstApiMVC.DBContexts.Models;

public partial class Item
{
    public int ItemId { get; set; }

    public string? ItemName { get; set; }

    public int? NumStockQuantity { get; set; }

    public bool? IsActive { get; set; }

    public string? ImageUrl { get; set; }

    public string? FileUrl { get; set; }
}
