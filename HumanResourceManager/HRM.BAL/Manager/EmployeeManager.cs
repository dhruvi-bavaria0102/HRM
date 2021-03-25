using HRM.Data.Models;
using HRM.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.BAL.Manager
{
    public class EmployeeManager : IEmployeeManager
    {
        IEmployeeRepository _employeeRepository;
        public EmployeeManager(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public void DeleteEmployee(long id)
        {
            _employeeRepository.DeleteEmployee(id);
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employeeRepository.GetAllEmployees();
        }

        public Employee GetEmployee(long id)
        {
            return _employeeRepository.GetEmployee(id);
        }

        public void SaveEmployee(Employee employee)
        {
            _employeeRepository.SaveEmployee(employee);
        }

        public void UpdateEmployee(Employee employee)
        {
            _employeeRepository.UpdateEmployee(employee);
        }

        public void Save()
        {
            _employeeRepository.Save();
        }
    }
}
