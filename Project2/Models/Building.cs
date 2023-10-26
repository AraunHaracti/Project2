namespace Project2.Models;

public partial class Building
{
    public int Id { get; set; }

    public int? StateId { get; set; }

    public int? ResidentialComplexId { get; set; }

    public string? Name { get; set; }
}
