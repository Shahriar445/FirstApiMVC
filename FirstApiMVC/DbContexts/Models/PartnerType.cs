using System;
using System.Collections.Generic;

namespace FirstApiMVC.DBContexts.Models;

public partial class PartnerType
{
    public int PartnerTypeId { get; set; }

    public string? PartnerTypeName { get; set; }

    public bool? IsActive { get; set; }
}
