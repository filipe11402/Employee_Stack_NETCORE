using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_Stack.Models.ViewModel
{
    public class EmployeeTechStackViewModel
    {
        public int LanguageId { get; set; }
        public string LanguageName { get; set; }
        public bool IsKnown { get; set; }

    }
}
