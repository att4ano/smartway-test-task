namespace Application.Dto.ResponseDto;

public record EmployeeDtoResponse(
    long Id,
    string Name, 
    string Surname, 
    string Phone,
    long CompanyId,
    long DepartmentId,
    long PassportId);