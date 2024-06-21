using Application.Dto.RequestDto;
using Application.Dto.ResponseDto;

namespace Contracts;

public interface IEmployeeService
{
    Task<IEnumerable<EmployeeDtoResponse>> GetAll();

    Task<IEnumerable<EmployeeDtoResponse>> GetAllByCompanyId(EmployeeByIdDtoRequest request);

    Task<IEnumerable<EmployeeDtoResponse>> GetAllByDepartmentId(EmployeeByIdDtoRequest request);

    Task<EmployeeIdDtoResponse> Add(AddEmployeeDtoRequest request);
    
    Task Delete(EmployeeByIdDtoRequest request);

    Task Update(UpdateEmployeeDtoRequest request);
}