namespace Application.Dto.RequestDto;

public record AddEmployeeDtoRequest(
    string Name, 
    string Surname, 
    string Phone,
    long CompanyId,
    long DepartmentId,
    long PassportId);