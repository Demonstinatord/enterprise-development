
namespace VetClinic.Domain;

public class Pet
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required Species Type { get; set; }
    public required DateOnly BirthDate { get; set; }
    public int BreedId { get; set; }
    public required Breed Breed { get; set; }
    public required int weight { get; set; }
    public int OwnerId { get; set; }
    public required Owner Owner { get; set; }


}