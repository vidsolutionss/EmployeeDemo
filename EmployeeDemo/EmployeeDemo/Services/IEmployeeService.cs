using EmployeeDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeDemo.Services
{
    public interface IEmployeeService
    {
        Task<Employees> GetEmployeeById(int id);
        Task<List<Employees>> GetEmployees();
        Task<bool> UpdateEmployee(Employees employees);
        Task<bool> DeleteEmployee(int id);
        Task<bool> InsertEmployee(Employees employees);
        Task<Users> GetUserInfo(string userName, string passwrod);
    }
}
