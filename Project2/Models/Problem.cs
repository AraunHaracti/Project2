using System;
using System.Collections.Generic;

namespace Project2.Models;

public partial class Problem
{
    public int Id { get; set; }

    public int? PlaceId { get; set; }

    public DateTime? DateAdded { get; set; }

    public DateTime? DateCompleted { get; set; }

    public string? Description { get; set; }

    public int? ProblemPriorityId { get; set; }

    public int? ProblemStatusId { get; set; }
}
