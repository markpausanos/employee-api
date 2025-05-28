using System.Data;
using Dapper;
using EmployeeAPI.Models;
using EmployeeAPI.Repositories.Interfaces;

namespace EmployeeAPI.Repositories;

internal class EmployeeRepository(
    IDbConnection connection,
    ILogger<EmployeeRepository> logger) : IEmployeeRepository
{
    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
        const string sql = "SELECT * FROM Employees";
        return await connection.QueryAsync<Employee>(sql);
    }

    public async Task<Employee?> GetByIdAsync(int id)
    {
        const string sql = "SELECT * FROM Employees WHERE Id = @Id";
        return await connection.QueryFirstOrDefaultAsync<Employee?>(sql, new { Id = id });
    }

    public async Task<int?> AddAsync(Employee employee)
    {
        const string sql = "INSERT INTO Employees (FirstName, MiddleName, LastName) " +
                           "VALUES (@FirstName, @MiddleName, @LastName); " +
                           "SELECT CAST(SCOPE_IDENTITY() as int)";
        try
        {
            return await connection.ExecuteScalarAsync<int>(sql, employee);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error adding employee");
            return null;
        }
    }

    public async Task<bool> UpdateAsync(Employee employee)
    {
        const string sql = "UPDATE Employees SET FirstName = @FirstName, " +
                           "MiddleName = @MiddleName, LastName = @LastName " +
                           "WHERE Id = @Id";

        try
        {
            var rowsAffected = await connection.ExecuteAsync(sql, employee);

            return rowsAffected > 0;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error updating employee");
            return false;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        const string sql = "DELETE FROM Employees WHERE Id = @Id";

        try
        {
            var rowsAffected = await connection.ExecuteAsync(sql, new { Id = id });

            return rowsAffected > 0;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error deleting employee");
            return false;
        }
    }
}