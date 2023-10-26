namespace Project2.Models;

public partial class EmployeePositionInSystem
{
    public int Id { get; set; }

    public int? EmployeeId { get; set; }

    public int? PositionInSystemId { get; set; }
}
