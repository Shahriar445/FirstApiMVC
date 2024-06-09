using System;
using System.Collections.Generic;

namespace FirstApiMVC.DBContexts.Models;

public partial class Purchase
{
    public int PurchaseId { get; set; }

    public int? SupplierId { get; set; }

    public DateTime? PurchaseDate { get; set; }

    public bool? IsActive { get; set; }
}
