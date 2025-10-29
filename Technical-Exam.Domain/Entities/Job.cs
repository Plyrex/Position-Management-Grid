using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technical_Exam.Domain.Entities
{
    public class Job
    {
        public required int JobId{ get; set; }
        public required string name{ get; set; }
        public string? description{ get; set; }
    }
}
