using Application.Dto.RequestDto;
using Application.Dto.ResponseDto;
using Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("employee")]
public class EmployeeController
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet]
    [Route("")]
    public async Task<IEnumerable<EmployeeDtoResponse>> GetAll()
    {
        var response = await _employeeService.GetAll();
        return response;
    }
    
    [HttpGet]
    [Route("company/{id:long}")]
    public async Task<IEnumerable<EmployeeDtoResponse>> GetAllByCompanyId(long id)
    {
        var response = await _employeeService.GetAllByCompanyId(new EmployeeByIdDtoRequest(id));
        return response;
    }
    
    [HttpGet]
    [Route("department/{id:long}")]
    public async Task<IEnumerable<EmployeeDtoResponse>> GetAllByDepartmentId(long id)
    {
        var response = await _employeeService.GetAllByDepartmentId(new EmployeeByIdDtoRequest(id));
        return response;
    }
    
    [HttpPost]
    [Route("")]
    public async Task<EmployeeIdDtoResponse> Add([FromBody] AddEmployeeDtoRequest request)
    {
        var response = await _employeeService.Add(request);
        return response;
    }

    [HttpDelete]
    [Route("{id:long}")]
    public async Task Delete(long id)
    {
        await _employeeService.Delete(new EmployeeByIdDtoRequest(id));
    }

    [HttpPut]
    [Route("")]
    public async Task Update(UpdateEmployeeDtoRequest request)
    {
        await _employeeService.Update(request);
    }
}