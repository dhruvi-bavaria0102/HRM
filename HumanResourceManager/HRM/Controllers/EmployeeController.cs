using HRM.BAL.Manager;
using HRM.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRM.Controllers
{
    public class EmployeeController : Controller
    {
        IEmployeeManager _employeeManager;

        public EmployeeController(IEmployeeManager employeeManager)
        {
            _employeeManager = employeeManager;
        }

        [Authorize]
        [ResponseCache(Duration = 1)]
        // Get all Employees
        public IActionResult Index()
        {
            var employee = _employeeManager.GetAllEmployees();
            return View(employee);
        }

        //Give details of selected employee
        public IActionResult Details(int id)
        {
            var employee = _employeeManager.GetEmployee(id);
            return View(employee);
        }

        public IActionResult Create()
        {
            return View();
        }

        //Creates new employee entry
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _employeeManager.SaveEmployee(employee);
                
                return RedirectToAction("Index");
            }
            return View();
        }

        //Get employee details to edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var employee = _employeeManager.GetEmployee(id);
            return View(employee);
        }
        //Saves edited details of employee
        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _employeeManager.UpdateEmployee(employee);
                //_employeeManager.SaveEmployee(employee);
                return RedirectToAction("Index", "Employee");

            }
            else
            {
                return View(employee);
            }
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var employee = _employeeManager.GetEmployee(id);
            return View(employee);
        }

        //Delete employee
        [HttpPost]
        public IActionResult Delete(int id,IFormCollection form)
        {
            _employeeManager.DeleteEmployee(id);
           
            return RedirectToAction("Index", "Employee");
        }
    }
}
