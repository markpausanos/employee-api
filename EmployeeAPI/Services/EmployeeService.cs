using EmployeeAPI.DTOs.Employee;
using EmployeeAPI.Mappers;
using EmployeeAPI.Models;
using EmployeeAPI.Repositories.Interfaces;
using EmployeeAPI.Services.Interfaces;

namespace EmployeeAPI.Services;

internal class EmployeeService(
    IEmployeeRepository repository,
    ILogger<EmployeeService> logger
) : IEmployeeService
{
    public async Task<IEnumerable<EmployeeResponse>> GetAllAsync()
    {
        var employees = await repository.GetAllAsync();
        return employees.Select(e => e.ToResponse());
    }

    public async Task<EmployeeResponse?> GetByIdAsync(int id)
    {
        var employee = await repository.GetByIdAsync(id);
        return employee?.ToResponse();
    }

    public async Task<int?> AddAsync(CreateEmployeeRequest request)
    {
        var employee = new Employee
        {
            FirstName = request.FirstName,
            MiddleName = request.MiddleName,
            LastName = request.LastName
        };

        return await repository.AddAsync(employee);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await repository.DeleteAsync(id);
    }

    public async Task<EmployeeResponse?> UpdateAsync(int id, UpdateEmployeeRequest request)
    {
        var employee = new Employee
        {
            Id = id,
            FirstName = request.FirstName,
            MiddleName = request.MiddleName,
            LastName = request.LastName
        };

        var updated = await repository.UpdateAsync(employee);
        if (!updated) return null;

        var updatedEmployee = await repository.GetByIdAsync(id);
        return updatedEmployee?.ToResponse();
    }
}