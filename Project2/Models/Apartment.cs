namespace Project2.Models;

public partial class Apartment
{
    public int Id { get; set; }

    public int? StateId { get; set; }

    public int? BuildingId { get; set; }

    public string? Name { get; set; }
}
