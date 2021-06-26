using Employee_Stack.Data;
using Employee_Stack.Models;
using Employee_Stack.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_Stack.Controllers
{
    public class EmployeeController : Controller
    {

        private readonly ApplicationDbContext _db;

        public EmployeeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {

            var EmpList = _db.Employees;


            return View(EmpList);
        }

        //GET method
        public IActionResult Create() 
        {

            //create private function that creates object in VM and define True/False based at what languages are known by that employee

            return View();
        }

        [HttpPost]
        public IActionResult CreatePost(EmployeeTechStackViewModel newEmployee)
        {

            if (ModelState.IsValid) 
            {
                _db.Add(newEmployee);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}
