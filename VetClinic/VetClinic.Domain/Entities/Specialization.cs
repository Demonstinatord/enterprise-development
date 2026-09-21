using VetClinic.Domain;

public class Specialization
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required Species TargetSpecies { get; set; } 
}
