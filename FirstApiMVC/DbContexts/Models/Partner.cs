using System;
using System.Collections.Generic;

namespace FirstApiMVC.DBContexts.Models;

public partial class Partner
{
    public int PartnerId { get; set; }

    public string? PartnerName { get; set; }

    public int? PartnerTypeId { get; set; }

    public bool? IsActive { get; set; }
}
