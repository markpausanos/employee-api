using EmployeeAPI.DTOs.Employee;

namespace EmployeeAPI.Services.Interfaces;

public interface IEmployeeService
{
    Task<IEnumerable<EmployeeResponse>> GetAllAsync();
    Task<EmployeeResponse?> GetByIdAsync(int id);
    Task<int?> AddAsync(CreateEmployeeRequest request);
    Task<EmployeeResponse?> UpdateAsync(int id, UpdateEmployeeRequest request);
    Task<bool> DeleteAsync(int id);
}