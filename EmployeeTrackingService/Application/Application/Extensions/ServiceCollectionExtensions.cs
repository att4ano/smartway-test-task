using Application.Services;
using Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ServiceCollectionExtensions
{
   public static IServiceCollection AddApplication(this IServiceCollection collection)
   {
      collection.AddScoped<IEmployeeService, EmployeeService>();
      return collection;
   }
}