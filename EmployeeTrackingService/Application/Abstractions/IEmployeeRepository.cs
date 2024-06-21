using Models.Models;


namespace Abstractions;

public interface IEmployeeRepository
{
    Task<IEnumerable<Employee>> GetAll();
    
    Task<IEnumerable<Employee>> GetAllByCompanyId(long id);

    Task<IEnumerable<Employee>> GetAllByDepartmentId(long id);
    
    Task<IEnumerable<long>> Add(IEnumerable<Employee> request);

    Task Delete(long id);

    Task Update(long id, Dictionary<string, object> fieldsToUpdate);
}