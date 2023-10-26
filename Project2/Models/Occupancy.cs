using System;
using System.Collections.Generic;

namespace Project2.Models;

public partial class Occupancy
{
    public int Id { get; set; }

    public int? TenantId { get; set; }

    public int? ApartmentId { get; set; }

    public DateTime? DateBegin { get; set; }

    public DateTime? DateEnd { get; set; }

    public int? OccupancyTypeId { get; set; }
}
