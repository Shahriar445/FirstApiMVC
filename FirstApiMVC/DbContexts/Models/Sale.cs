using System;
using System.Collections.Generic;

namespace FirstApiMVC.DBContexts.Models;

public partial class Sale
{
    public int SalesId { get; set; }

    public int? CustomerId { get; set; }

    public DateTime? SalesDate { get; set; }

    public bool? IsActive { get; set; }
}
