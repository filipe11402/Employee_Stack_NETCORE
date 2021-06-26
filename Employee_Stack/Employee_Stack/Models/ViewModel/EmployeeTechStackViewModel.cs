using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_Stack.Models.ViewModel
{
    public class EmployeeTechStackViewModel
    {
        public Employee Employee { get; set; }
        public string Languages { get; set; }
        public bool KnownLanguage { get; set; }

    }
}
