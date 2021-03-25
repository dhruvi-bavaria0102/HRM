using HRM.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.BAL.Manager
{
    public interface IEmployeeManager
    {
        void SaveEmployee(Employee employee);
        IEnumerable<Employee> GetAllEmployees();
        Employee GetEmployee(long id);
        void DeleteEmployee(long id);
        void UpdateEmployee(Employee employee);
        void Save();
    }
}
