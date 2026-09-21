namespace VetClinic.Domain;

public class Breed
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required Species Type { get; set; }
}
