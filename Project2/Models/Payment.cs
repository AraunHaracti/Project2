using System;
using System.Collections.Generic;

namespace Project2.Models;

public partial class Payment
{
    public int Id { get; set; }

    public int? ApartmentId { get; set; }

    public double? Amount { get; set; }

    public DateTime? Date { get; set; }

    public int? PaymentTypeId { get; set; }

    public string? Description { get; set; }
}
