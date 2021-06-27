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

            var EmpList = _db.Employees.Include(x => x.TeckStack);


            return View(EmpList);
        }

        //GET method
        public IActionResult Create() 
        {
            var newEmployee = new Employee();
            newEmployee.TeckStack = new List<TechStack>();
            PopulateLanguageData(newEmployee);


            return View();
        }

        //POST method
        [HttpPost]
        public IActionResult CreatePost(Employee newEmployee, string[] selectedLanguages) 
        {
            if (selectedLanguages != null) 
            {
                newEmployee.TeckStack = new List<TechStack>();

                foreach (var language in selectedLanguages) 
                {
                    var languageToAdd = _db.TeckStack.Find(int.Parse(language));
                    newEmployee.TeckStack.Add(languageToAdd);
                }
            }

            if (ModelState.IsValid) 
            {
                _db.Employees.Add(newEmployee);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            PopulateLanguageData(newEmployee);
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

        //POST method
        [HttpPost]
        public IActionResult EditPost(int? Id, string[] selectedLanguages)
        {

            if (Id == null || Id <= 0)
            {
                return NotFound();
            }

            Employee employeeToUpdate = _db.Employees.Include(x => x.TeckStack).Where(x => x.Id == Id).Single();

            
            if (employeeToUpdate == null)
            {
                return NotFound();
            }

            UpdateLanguageData(selectedLanguages, employeeToUpdate);

            if (ModelState.IsValid) 
            {
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            

            return View(employeeToUpdate);
        }

        //GET method
        public IActionResult Delete(int? Id) 
        {
            Employee empToDelete = _db.Employees.Include(x => x.TeckStack).FirstOrDefault(x => x.Id == Id);

            if (empToDelete == null) 
            {
                return NotFound();
            }

            return View(empToDelete);
        }

        //POST method
        [HttpPost]
        public IActionResult DeletePost(int? Id)
        {
            Employee empToDelete = _db.Employees.Include(x => x.TeckStack).FirstOrDefault(x => x.Id == Id);

            if (empToDelete == null)
            {
                return NotFound();
            }

            empToDelete.TeckStack.Clear();
            _db.Remove(empToDelete);
            _db.SaveChanges();

            return RedirectToAction("Index");
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

        private void UpdateLanguageData(string[] selectedLanguages, Employee employeeToUpdate) 
        {
            if (selectedLanguages == null) 
            {
                employeeToUpdate.TeckStack = new List<TechStack>();
            }

            //creating hashset to retrieve what languages where selected and the one that already exists
            var selectedLanguagesHS = new HashSet<String>(selectedLanguages);
            var currentEmployeeLanguagesHS = new HashSet<int>(employeeToUpdate.TeckStack.Select(x => x.Id));

            foreach(var language in _db.TeckStack) 
            {
                if (selectedLanguagesHS.Contains(language.Id.ToString()))
                {
                    if (!currentEmployeeLanguagesHS.Contains(language.Id))
                    {
                        employeeToUpdate.TeckStack.Add(language);
                    }
                }
                else 
                {
                    if (currentEmployeeLanguagesHS.Contains(language.Id)) 
                    {
                        employeeToUpdate.TeckStack.Remove(language);
                    }
                }
            }
        }
    }
}
