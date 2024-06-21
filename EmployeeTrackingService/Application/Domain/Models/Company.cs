namespace Models.Models;

public record Company
{
    public required long Id { get; init; }
    public required string Name { get; init; }
}