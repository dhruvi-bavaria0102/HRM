using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HRM.Data.Models
{
    public class Employee
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public decimal Salary { get; set; }
        public bool IsManager { get; set; }
        public string Manager { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
