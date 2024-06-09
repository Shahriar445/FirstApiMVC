using System;
using System.Collections.Generic;

namespace FirstApiMVC.DBContexts.Models;

public partial class PurchaseDetail
{
    public int DetailsId { get; set; }

    public int? PurchaseId { get; set; }

    public int? ItemId { get; set; }

    public decimal? ItemQuantity { get; set; }

    public decimal? UnitPrice { get; set; }

    public bool? IsActive { get; set; }
}
