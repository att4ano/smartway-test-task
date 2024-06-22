namespace Models.Models;

public record Passport
{
    public long Id { get; init; }
    public required string Type { get; init; }
    public required string Number { get; init; }
}