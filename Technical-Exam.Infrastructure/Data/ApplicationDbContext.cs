using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technical_Exam.Domain.Entities;

namespace Technical_Exam.Infrastructure.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }

        public DbSet<Position> Positions { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Location> Locations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Enable Departments to use the self-referencing key
            //I learned this off of Microsoft Learn
            modelBuilder.Entity<Department>()
                .HasOne(d => d.ParentDepartment)
                .WithMany(p => p.SubDepartments)
                .HasForeignKey(d => d.ParentDepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            //seed Departments
            modelBuilder.Entity<Department>().HasData(
                new Department {DepartmentId= 1, name= "Information Technology", ParentDepartmentId= null, locationId= 1},
                new Department {DepartmentId= 2, name= "Human Resources", ParentDepartmentId= null, locationId= 1},
                new Department { DepartmentId = 3, name = "Application Development", ParentDepartmentId = 1, locationId = 2 }
            );

            //seed Jobs
            modelBuilder.Entity<Job>().HasData(
                new Job {JobId= 1, name= "Software Developer", description= "Develops Software"},
                new Job {JobId= 2, name= "HR Manager", description= null}
            );

            //seed Employees
            modelBuilder.Entity<Employee>().HasData(
                new Employee {EmployeeId= 1, FirstName= "Jane", LastName="Doe", PhoneNumber="987-0987", Email= "janedoe@gmail.com", Address="123 Test Road, Arima"},
                new Employee {EmployeeId= 2, FirstName= "John", LastName="Doe", PhoneNumber="123-4567", Email= "johndoe@gmail.com", Address="123 Test Road, El Socorro"},
                new Employee {EmployeeId= 3, FirstName= "Saeed", LastName="Khan", PhoneNumber="765-4321", Email= "saeedkhan@gmail.com", Address="123 Test Road, Chaguanas"}
            );

            //seed Locations
            modelBuilder.Entity<Location>().HasData(
                new Location {LocationId= 1, name= "El Socorro", address= "2B Chootoo Road"},
                new Location {LocationId= 2, name= "Chaguanas", address= "Ramsaran Street"}
            );

            //Enable Position to use employee ID's in createdBy and updatedBy columns
            //Also learned this off Microsoft Learn
            modelBuilder.Entity<Position>()
                .HasOne(p => p.CreatedBy)
                .WithMany()
                .HasForeignKey(p => p.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Position>()
                .HasOne(p => p.UpdatedBy)
                .WithMany()
                .HasForeignKey(p => p.UpdatedById)
                .OnDelete(DeleteBehavior.Restrict);

            //auto-increment the ID when adding
            modelBuilder.Entity<Position>()
                .Property(p => p.PositionId)
                .ValueGeneratedOnAdd();


            //seed Position
            modelBuilder.Entity<Position>().HasData(
                new Position
                {
                    PositionId = 1,
                    PositionName = "Software Developer",
                    DepartmentId = 3,
                    LocationId = 1,
                    JobId = 1,
                    EmploymentType = Domain.Enums.EmploymentType.FullTime,
                    EmploymentStatus = Domain.Enums.EmploymentStatus.Active,
                    EmployeeId = 2,
                    CreatedById = 1,
                    TimeCreated = new DateTime(2025, 10, 20, 0, 0, 0, DateTimeKind.Utc),
                    UpdatedById = null,
                    LastUpdated = null
                }
            );

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=TechnicalExamDB.db");
        }

       
    }
}
