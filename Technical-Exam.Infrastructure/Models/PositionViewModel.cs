using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technical_Exam.Infrastructure.Models
{
    public class PositionViewModel
    {
        public IEnumerable<DropDownMenu>? Departments { get; set; }
        public IEnumerable<DropDownMenu>? Locations { get; set; }
        public IEnumerable<DropDownMenu>? Jobs { get; set; }
        public IEnumerable<DropDownMenu>? Employees { get; set; }
    }

    public class DropDownMenu
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}
