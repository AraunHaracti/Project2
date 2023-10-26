namespace Project2.Models;

public partial class Place
{
    public int Id { get; set; }

    public int? ApartmentId { get; set; }

    public int? BuildingId { get; set; }

    public int? ResidentialComplexId { get; set; }
}
