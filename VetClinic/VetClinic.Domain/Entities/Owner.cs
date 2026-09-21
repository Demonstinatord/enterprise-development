namespace VetClinic.Domain;

public class Owner
{
    public int Id { get; set; }
    public required string FullName { get; set; }
    public required string Phone { get; set; }
    public required string Adress { get; set; }
    
}