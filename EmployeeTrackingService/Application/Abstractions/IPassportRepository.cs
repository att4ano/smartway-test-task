using Models.Models;

namespace Abstractions;

public interface IPassportRepository
{
    Task<IEnumerable<Passport>> GetAll();
    
    Task<IEnumerable<long>> Add(IEnumerable<Passport> request);

    Task Delete(long id);
}