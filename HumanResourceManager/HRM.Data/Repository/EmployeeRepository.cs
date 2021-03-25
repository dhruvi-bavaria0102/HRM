using HRM.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Data.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private ApplicationDBContext context;
        private DbSet<Employee> studentEntity;
        public EmployeeRepository(ApplicationDBContext context)
        {
            this.context = context;
            studentEntity = context.Set<Employee>();
        }
        public void DeleteEmployee(long id)
        {
            Employee employee = GetEmployee(id);
            studentEntity.Remove(employee);
            context.SaveChanges();
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return studentEntity.AsEnumerable();
        }

        public  Employee GetEmployee(long id)
        {
            return  studentEntity.SingleOrDefault(s => s.ID == id);
        }

        public void SaveEmployee(Employee employee)
        {
            context.Entry(employee).State = EntityState.Added;
            context.SaveChanges();
        }

        public void UpdateEmployee(Employee employee)
        {
            context.Entry(employee).State = EntityState.Modified;
            context.SaveChanges();
        }
        public void Save()
        {
            context.SaveChanges();
        }

        
    }
}
