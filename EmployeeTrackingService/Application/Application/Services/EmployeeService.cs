using System.Transactions;
using Abstractions;
using Application.Dto.RequestDto;
using Application.Dto.ResponseDto;
using Application.Mapper;
using Contracts;
using Models.Models;

namespace Application.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IPassportRepository _passportRepository;

    public EmployeeService(IEmployeeRepository employeeRepository, 
        IPassportRepository passportRepository)
    {
        _employeeRepository = employeeRepository;
        _passportRepository = passportRepository;
    }

    private static TransactionScope CreateTransactionScope(
        IsolationLevel level = IsolationLevel.ReadCommitted)
    {
        return new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions 
            { 
                IsolationLevel = level, 
                Timeout = TimeSpan.FromSeconds(5) 
            },
            TransactionScopeAsyncFlowOption.Enabled);
    }
    
    public async Task<IEnumerable<EmployeeDtoResponse>> GetAll()
    {
        using var transaction = CreateTransactionScope();
        var response = await _employeeRepository.GetAll();
        transaction.Complete();
        return response.Select(e => e.AsDto());
    }

    public async Task<IEnumerable<EmployeeDtoResponse>> GetAllByCompanyId(EmployeeByIdDtoRequest request)
    {
        using var transaction = CreateTransactionScope();
        var response = await _employeeRepository.GetAllByCompanyId(request.Id);
        transaction.Complete();
        return response.Select(e => e.AsDto());
    }

    public async Task<IEnumerable<EmployeeDtoResponse>> GetAllByDepartmentId(EmployeeByIdDtoRequest request)
    {
        using var transaction = CreateTransactionScope();
        var response = await _employeeRepository.GetAllByDepartmentId(request.Id);
        transaction.Complete();
        return response.Select(e => e.AsDto());
    }

    public async Task<EmployeeIdDtoResponse> Add(AddEmployeeDtoRequest request)
    {
        using var transaction = CreateTransactionScope();

        var passport = new Passport
        {
            Type = request.PassportType,
            Number = request.PassportNumber,
        };

        var passportIdResponse = await _passportRepository.Add(new[] { passport });
        
        var employee = new Employee
        {
            Name = request.Name,
            Surname = request.Surname,
            Phone = request.Phone,
            CompanyId = request.CompanyId,
            DepartmentId = request.DepartmentId,
            PassportId = passportIdResponse.Single()
        };
        
        var response = await _employeeRepository.Add(new[] { employee });
        transaction.Complete();
        return new EmployeeIdDtoResponse(response.Single());
    }

    public async Task Delete(EmployeeByIdDtoRequest request)
    {
        using var transaction = CreateTransactionScope();
        await _employeeRepository.Delete(request.Id);
        transaction.Complete();
    }

    public async Task Update(UpdateEmployeeDtoRequest request)
    {
        var employeeFieldsToUpdate = new Dictionary<string, object>();

        if (!string.IsNullOrEmpty(request.Name))
            employeeFieldsToUpdate.Add("name", request.Name);

        if (!string.IsNullOrEmpty(request.Surname))
            employeeFieldsToUpdate.Add("surname", request.Surname);

        if (!string.IsNullOrEmpty(request.Phone))
            employeeFieldsToUpdate.Add("phone", request.Phone);

        if (request.CompanyId != null)
            employeeFieldsToUpdate.Add("company_id", request.CompanyId);

        if (request.DepartmentId != null)
            employeeFieldsToUpdate.Add("department_id", request.DepartmentId);
        
        using var transaction = CreateTransactionScope();
        await _employeeRepository.Update(request.Id, employeeFieldsToUpdate);
        transaction.Complete();
    }
}