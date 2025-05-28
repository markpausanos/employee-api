using EmployeeAPI.DTOs.Employee;
using EmployeeAPI.Models;

namespace EmployeeAPI.Mappers;

public static class EmployeeMapper
{
    public static EmployeeResponse ToResponse(this Employee employee)
    {
        return new EmployeeResponse
        {
            Id = employee.Id,
            FirstName = employee.FirstName,
            MiddleName = employee.MiddleName,
            LastName = employee.LastName
        };
    }
}