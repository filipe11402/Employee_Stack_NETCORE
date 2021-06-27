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

        //GET method
        public IActionResult Edit(int? Id) 
        {

            if (Id == null || Id <= 0) 
            {
                return NotFound();
            }

            Employee employee = _db.Employees.Include(x => x.TeckStack).Where(x => x.Id == Id).Single();

            PopulateLanguageData(employee);
            if (employee == null) 
            {
                return NotFound();
            }

            return View(employee);
        }

        //Populating data in viewmodel to show checkboxes/checked checkboxes
        private void PopulateLanguageData(Employee employee) 
        {
            var allLanguages = _db.TeckStack;
            var allEmployeeLanguages = new HashSet<int>(employee.TeckStack.Select(x => x.Id));
            var viewModel = new List<EmployeeTechStackViewModel>();

            foreach (var language in allLanguages) 
            {
                viewModel.Add(new EmployeeTechStackViewModel
                {
                    LanguageId = language.Id,
                    LanguageName = language.Languages,
                    IsKnown = allEmployeeLanguages.Contains(language.Id)
                }
                );
            }

            ViewBag.Languages = viewModel;
        }
    }
}
