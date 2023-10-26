using System;
using System.Collections.Generic;

namespace Project2.Models;

public partial class Disbursement
{
    public int Id { get; set; }

    public int? PaymentId { get; set; }

    public int? TenantId { get; set; }

    public double? Amount { get; set; }

    public DateTime? Date { get; set; }
}
