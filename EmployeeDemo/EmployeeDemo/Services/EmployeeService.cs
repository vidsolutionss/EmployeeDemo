using EmployeeDemo.Models;
using ServiceProviderAPI.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeDemo.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employees> _employeeServiceRepo;
        private readonly IRepository<Users> _userRepo;

        public EmployeeService(IRepository<Employees> employeeServiceRepo, IRepository<Users> userRepo)
        {
            _employeeServiceRepo = employeeServiceRepo;
            _userRepo = userRepo;
        }

        public async Task<bool> InsertEmployee(Employees employees)
        {
            await _employeeServiceRepo.Add(employees);

            return await _employeeServiceRepo.Save() > 0 ? true : false;
        }        

        public async Task<Employees> GetEmployeeById(int id)
        {            
            var modelList = (await _employeeServiceRepo.Get(item => item.Id == id)).FirstOrDefault();            
            return modelList;
        }

        public async Task<List<Employees>> GetEmployees()
        {
            var modelList = (await _employeeServiceRepo.GetAll()).ToList();
            return modelList;
        }

        public async Task<bool> UpdateEmployee(Employees employees)
        {
            var data = await _employeeServiceRepo.GetById(employees.Id);
            if (data != null)
            {

                data.Name = employees.Name;
                    data.Salary = employees.Salary;
                data.EmployeeCode = employees.EmployeeCode;
               
                _employeeServiceRepo.Update(data);
                return await _employeeServiceRepo.Save() > 0 ? true : false;
            }
            return false;
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            var data = await _employeeServiceRepo.GetById(id);
            if (data != null)
            {
                _employeeServiceRepo.Delete(data);
                return await _employeeServiceRepo.Save() > 0 ? true : false;
            }
            return false;
        }

        public async Task<Users> GetUserInfo(string userName, string passwrod)
        {
            var modelList = (await _userRepo.Get(item => item.UserName == userName && item.Password == passwrod)).FirstOrDefault();
            return modelList;
        }
    }
}
