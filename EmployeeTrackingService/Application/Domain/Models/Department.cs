namespace Models.Models;

public record Department
{
    public required long Id { get; init; }
    public required string Name { get; init; }
    public required string PhoneNumber { get; init; }
}