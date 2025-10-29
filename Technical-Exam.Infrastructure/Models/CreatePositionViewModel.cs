using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technical_Exam.Domain.Entities;
using Technical_Exam.Domain.Enums;

namespace Technical_Exam.Infrastructure.Models
{
    public class CreatePositionViewModel
    {
        public required int PositionId { get; set; } //primary key
        public required string PositionName { get; set; }
        public required int DepartmentId { get; set; } //foreign key
        public required int LocationId { get; set; } //foreign key
        public required int JobId { get; set; } //foreign key
        public required EmploymentType EmploymentType { get; set; }
        public required EmploymentStatus EmploymentStatus { get; set; }
        public int? EmployeeId { get; set; } //foreign key
        public required int CreatedById { get; set; } //foreign key
        public Employee? CreatedBy { get; set; }
        public required DateTime TimeCreated { get; set; }
        public int? UpdatedById { get; set; } //foreign key
        public Employee? UpdatedBy { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}
