using EmployeeAPI.Models;

namespace EmployeeAPI.Repositories.Interfaces;

public interface IEmployeeRepository
{
    Task<IEnumerable<Employee>> GetAllAsync();
    Task<Employee?> GetByIdAsync(int id);
    Task<int?> AddAsync(Employee employee);
    Task<bool> UpdateAsync(Employee employee);
    Task<bool> DeleteAsync(int id);
}