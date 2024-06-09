using System;
using System.Collections.Generic;

namespace FirstApiMVC.DBContexts.Models;

public partial class SalesDetail
{
    public int DetailsId { get; set; }

    public int? SalesId { get; set; }

    public int? ItemId { get; set; }

    public int? ItemQuantity { get; set; }

    public decimal? UnitPrice { get; set; }

    public bool? IsActive { get; set; }
}
