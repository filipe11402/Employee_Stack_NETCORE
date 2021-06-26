using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_Stack.Models
{
    public class Employee
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public string EmployeeName { get; set; }
        public ICollection<TechStack> TeckStack { get; set; }

    }
}
