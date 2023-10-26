using System;
using System.Collections.Generic;

namespace Project2.Models;

public partial class EmployeePosition
{
    public int Id { get; set; }

    public int? EmployeeId { get; set; }

    public int? PositionId { get; set; }

    public DateTime? Date { get; set; }
}
