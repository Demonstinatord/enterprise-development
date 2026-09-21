namespace VetClinic.Domain;

public class Service
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public decimal Price { get; set; }
    public int SpecializationId { get; set; }
    public required Specialization Specialization{ get; set; }
}