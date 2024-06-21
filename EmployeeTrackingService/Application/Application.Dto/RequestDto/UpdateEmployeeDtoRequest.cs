namespace Application.Dto.RequestDto;

public record UpdateEmployeeDtoRequest(
    long Id, 
    string? Name, 
    string? Surname, 
    string? Phone,
    long? CompanyId,
    long? DepartmentId);