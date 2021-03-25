using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRM.Data.Models;


namespace HRM.Data.Repository
{
    public interface IEmployeeRepository
    {
        void SaveEmployee(Employee employee);
        IEnumerable<Employee> GetAllEmployees();
        Employee GetEmployee(long id);
        void DeleteEmployee(long id);
        void UpdateEmployee(Employee employee);
        void Save();
       
    }
}
