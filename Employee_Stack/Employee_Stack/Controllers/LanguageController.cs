using Employee_Stack.Data;
using Employee_Stack.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_Stack.Controllers
{
    public class LanguageController : Controller
    {
        private readonly ApplicationDbContext _db;

        public LanguageController(ApplicationDbContext db) 
        {
            _db = db;
        }

        public IActionResult Index()
        {

            var allLanguages = _db.TeckStack;

            return View(allLanguages);
        }

        //GET method
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreatePost(TechStack newLanguage)
        {
            if (ModelState.IsValid) 
            {
                _db.Add(newLanguage);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }


            return View();
        }

        //GET method
        public IActionResult Update(int? Id)
        {

            if (Id == null || Id == 0) 
            {
                return NotFound();
            }

            TechStack Language = _db.TeckStack.Find(Id);

            if (Language == null) 
            {
                return NotFound();
            }

            return View(Language);
        }

        //GET method
        [HttpPost]
        public IActionResult UpdatePost(TechStack updatedLanguage)
        {

            if (ModelState.IsValid) 
            {
                _db.Update(updatedLanguage);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}
