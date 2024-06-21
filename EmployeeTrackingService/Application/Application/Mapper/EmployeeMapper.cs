using Application.Dto.ResponseDto;
using Models.Models;

namespace Application.Mapper;

public static class EmployeeMapper
{
    public static EmployeeDtoResponse AsDto(this Employee employee)
        => new EmployeeDtoResponse(
            employee.Id,
            employee.Name,
            employee.Surname,
            employee.Phone,
            employee.CompanyId,
            employee.DepartmentId,
            employee.PassportId);
}