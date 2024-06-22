using Abstractions;
using Dapper;
using Models.Models;
using Npgsql;

namespace Infrastructure.DataAccess.Repositories;

public class PassportRepository : IPassportRepository
{
    private readonly NpgsqlDataSource _dataSource;

    public PassportRepository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public async Task<IEnumerable<Passport>> GetAll()
    {
        await using var connection = await _dataSource.OpenConnectionAsync();
        const string query = """
                              SELECT *
                              FROM passport
                              """;
        var response = await connection.QueryAsync<Passport>(query);
        return response;
    }

    public async Task<IEnumerable<long>> Add(IEnumerable<Passport> request)
    {
        await using var connection = await _dataSource.OpenConnectionAsync();
        const string query = """
                             INSERT INTO passport (type, number)
                             SELECT type, number
                             FROM UNNEST(@Passports)
                             RETURNING id
                             """;
        var response = await connection.QueryAsync<long>(
            new CommandDefinition(
                query,
                new
                {
                    Passports = request
                }));

        return response;
    }

    public async Task Delete(long id)
    {
        const string sqlQuery = "DELETE FROM passport WHERE id = @Id";
        await using var connection = await _dataSource.OpenConnectionAsync();
        await connection.ExecuteAsync(
            sqlQuery,
            new { Id = id }
        );
    }

}