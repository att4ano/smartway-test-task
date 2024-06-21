using System.Text;
using Abstractions;
using Dapper;
using Models.Models;
using Npgsql;

namespace Infrastructure.DataAccess.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly NpgsqlDataSource _dataSource;

    public EmployeeRepository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public async Task<IEnumerable<Employee>> GetAll()
    {
        await using var connection = await _dataSource.OpenConnectionAsync();
        const string query = """
                             SELECT *
                             FROM employee
                             """;
        var response = await connection.QueryAsync<Employee>(query);
        return response;
    }

    public async Task<IEnumerable<Employee>> GetAllByCompanyId(long id)
    {
        await using var connection = await _dataSource.OpenConnectionAsync();
        const string query = """
                             SELECT *
                             FROM employee as e 
                             WHERE e.company_id = @Id
                             """;
        var response = await connection.QueryAsync<Employee>(
            query,
            new
            {
                Id = id
            });
        return response;
    }

    public async Task<IEnumerable<Employee>> GetAllByDepartmentId(long id)
    {
        await using var connection = await _dataSource.OpenConnectionAsync();
        const string query = """
                             SELECT *
                             FROM employee as e
                             WHERE e.department_id = @Id
                             """;
        var response = await connection.QueryAsync<Employee>(
            query,
            new
            {
                Id = id
            });
        return response;
    }

    public async Task<IEnumerable<long>> Add(IEnumerable<Employee> request)
    {
        await using var connection = await _dataSource.OpenConnectionAsync();
        const string query = @"INSERT INTO employee (name, surname, phone, company_id, passport_id, department_id)
                               SELECT name, surname, phone, company_id, passport_id, department_id
                               FROM UNNEST(@Employees)
                               RETURNING id";
        var response = await connection.QueryAsync<long>(
            new CommandDefinition(
                query,
                new
                {
                    Employees = request
                }));

        return response;
    }

    public async Task Delete(long id)
    {
        const string sqlQuery = @"DELETE FROM employee WHERE id = @Id";
        await using var connection = await _dataSource.OpenConnectionAsync();
        await connection.ExecuteAsync(
            new CommandDefinition(
                sqlQuery,
                new { Id = id }
            )
        );
    }

    public async Task Update(long id, Dictionary<string, object> fieldsToUpdate)
    {
        if (fieldsToUpdate.Count == 0)
            throw new ArgumentException("No fields to update provided.", nameof(fieldsToUpdate));

        var updateClause = new StringBuilder();
        var parameters = new DynamicParameters();

        foreach (var field in fieldsToUpdate)
        {
            if (updateClause.Length > 0)
                updateClause.Append(", ");

            updateClause.Append($"{field.Key} = @{field.Key}");
            parameters.Add($"@{field.Key}", field.Value);
        }

        const string sqlQuery = @"UPDATE employee SET {0} WHERE id = @Id";
        var finalQuery = string.Format(sqlQuery, updateClause.ToString());

        parameters.Add("@Id", id);

        await using var connection = await _dataSource.OpenConnectionAsync();
        await connection.ExecuteAsync(finalQuery, parameters);
    }
}