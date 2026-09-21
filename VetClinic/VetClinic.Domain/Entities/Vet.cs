using VetClinic.Domain;

public class Vet
{
    public int Id { get; set; }
    public required string FullName { get; set; }
    public required string PassportNumber { get; set; }
    public required DateOnly BirthDate { get; set; }
    public required int ExpirienceYears { get; set; }
    public int SpecializationId { get; set; }
    public required Specialization Specialization { get; set; }

}