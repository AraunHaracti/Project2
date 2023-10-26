using System;
using System.Collections.Generic;

namespace Project2.Models;

public partial class Tenant
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public DateTime? Birthday { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? PassportSeries { get; set; }

    public string? PassportNumber { get; set; }
}
