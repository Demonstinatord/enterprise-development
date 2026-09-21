namespace VetClinic.Domain;

public class Appointment
{
    public int Id { get; set; }
    public DateTime DateTime { get; set; }
    public string? Diagnosis { get; set; }
    public int PetId { get; set; }
    public required Pet Pet { get; set; }
    public int VetId { get; set; }
    public required Vet Vet { get; set; }
    public required int RoomNumber { get; set; }
    public int ServiceId { get; set; }
    public required Service Service { get; set; }
    public required RepeatVisitIndicator RepeatVisitIndicator { get; set; }
}