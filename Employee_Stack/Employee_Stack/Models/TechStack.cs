using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Employee_Stack.Models
{
    public class TechStack
    {
        public int Id { get; set; }
        public string Languages { get; set; }
        public virtual ICollection<Employee> Employee { get; set; }
    }
}
