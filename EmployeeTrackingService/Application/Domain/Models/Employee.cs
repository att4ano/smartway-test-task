namespace Models.Models;

public record Employee
{
    public long Id { get; init; }
    public required string Name { get; init; }
    public required string Surname { get; init; }
    public required string Phone { get; init; }
    public required long CompanyId { get; init; }
    public required long DepartmentId { get; init; }
    public required long PassportId { get; init; }
}