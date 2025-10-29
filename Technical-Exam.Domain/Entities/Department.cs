using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technical_Exam.Domain.Entities
{
    public class Department
    {
        public required int DepartmentId{ get; set; } //primary key
        public required string name{ get; set; }
        public int? ParentDepartmentId{ get; set; } //self-referencing foreign key
        public Department? ParentDepartment{ get; set; }
        public ICollection<Department>? SubDepartments{ get; set; }
        public required int locationId{ get; set; }
    }
}
