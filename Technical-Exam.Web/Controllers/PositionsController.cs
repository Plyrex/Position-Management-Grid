using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Technical_Exam.Domain.Entities;
using Technical_Exam.Domain.Enums;
using Technical_Exam.Infrastructure.Data;
using Technical_Exam.Infrastructure.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Technical_Exam.Web.Controllers
{
    public class PositionsController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public PositionsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        ////get all positions (READ)
        //[HttpGet]
        //public async Task<IActionResult> Index()
        //{
        //    var positions = await dbContext.Positions.ToListAsync();
        //    return View(positions);
        //}

        ////get the add position view
        //[HttpGet]
        //public IActionResult Add()
        //{
        //    return View();
        //}

        ////post a new position to the database (CREATE)
        //[HttpPost]
        //public async Task<IActionResult> Add(CreatePositionViewModel viewModel)
        //{
        //    var position = new Position
        //    {
        //        PositionName= viewModel.PositionName,
        //        DepartmentId=viewModel.DepartmentId,
        //        LocationId=viewModel.LocationId,
        //        JobId=viewModel.JobId,
        //        EmploymentType=viewModel.EmploymentType,
        //        EmploymentStatus=viewModel.EmploymentStatus,
        //        EmployeeId=viewModel.EmployeeId,
        //        CreatedById=viewModel.CreatedById,
        //        TimeCreated= DateTime.UtcNow
        //    };

        //    await dbContext.Positions.AddAsync(position);
        //    await dbContext.SaveChangesAsync();

        //    return View();
        //}

        ////get the edit position view
        //[HttpGet]
        //public async Task<IActionResult> Edit(int id)
        //{
        //    var position= await dbContext.Positions.FindAsync(id);

        //    return View(position);
        //}

        ////update a position in the database (UPDATE)
        //[HttpPost]
        //public async Task<IActionResult> Edit(Position viewModel)
        //{
        //    var position= await dbContext.Positions.FindAsync(viewModel.PositionId);
        //    if(position is not null)
        //    {
        //        position.PositionName = viewModel.PositionName;
        //        position.DepartmentId = viewModel.DepartmentId;
        //        position.LocationId = viewModel.LocationId;
        //        position.JobId = viewModel.JobId;
        //        position.EmploymentType = viewModel.EmploymentType;
        //        position.EmploymentStatus = viewModel.EmploymentStatus;
        //        position.EmployeeId = viewModel.EmployeeId;
        //        position.UpdatedById = viewModel.UpdatedById;
        //        position.LastUpdated = DateTime.UtcNow;

        //        await dbContext.SaveChangesAsync();
        //    }

        //    return RedirectToAction("Index", "Positions");
        //}

        ////delete a postion (DELETE)
        //[HttpPost]
        //public async Task<IActionResult> Delete(Position viewModel)
        //{
        //    var position= await dbContext.Positions.AsNoTracking().FirstOrDefaultAsync(x => x.PositionId== viewModel.PositionId);
        //    if (position is not null)
        //    {
        //        dbContext.Positions.Remove(viewModel);
        //        await dbContext.SaveChangesAsync();
        //    }

        //    return RedirectToAction("Index", "Positions");
        //}

        public IActionResult Index()
        {
            //found viewbags on kendo docs
            //This is for dropdowns based on the foreign keys

            var model = new PositionViewModel
            {
            //departments
            Departments= dbContext.Departments
                .Select(p => new DropDownMenu {Id= p.DepartmentId, Name= p.name})
                .ToList(),

            //locations
            Locations = dbContext.Locations
                .Select(p => new DropDownMenu { Id = p.LocationId, Name = p.name })
                .ToList(),

            //jobs
            Jobs = dbContext.Jobs
                .Select(p => new DropDownMenu { Id = p.JobId, Name = p.name })
                .ToList(),

            //employees
            Employees = dbContext.Employees
                .Select(p => new DropDownMenu{Id= p.EmployeeId, Name = p.FirstName + " " + p.LastName })
                .ToList()
            };

            //Viewdata to populate dropdowns
            ViewData["Departments"]= model.Departments;
            ViewData["Locations"]= model.Locations;
            ViewData["Jobs"]= model.Jobs;
            ViewData["Employees"]= model.Employees;

            ViewData["EmploymentTypes"]= Enum.GetValues(typeof(EmploymentType))
                .Cast<EmploymentType>()
                .Select(e => new DropDownMenu {Id= (int)e, Name= e.ToString()})
                .ToList();

            ViewData["EmploymentStatuses"]= Enum.GetValues(typeof(EmploymentStatus))
                .Cast<EmploymentStatus>()
                .Select(e => new DropDownMenu {Id= (int)e, Name= e.ToString()})
                .ToList();

            return View(model);
        }

        public async Task<IActionResult> Read([DataSourceRequest] DataSourceRequest request)
        {
            var positions = await dbContext.Positions.ToListAsync();

            return Json(positions.ToDataSourceResult(request));
        }

        public async Task<IActionResult> Create([DataSourceRequest] DataSourceRequest request, Position position)
        {
            if (position != null && ModelState.IsValid)
            {
                position.TimeCreated = DateTime.UtcNow; //sets current time at create
                dbContext.Positions.Add(position);
                await dbContext.SaveChangesAsync();
            }

            return Json(new[] { position }.ToDataSourceResult(request, ModelState));
        }

        public async Task<IActionResult> Update([DataSourceRequest] DataSourceRequest request, Position position)
        {
            if (position != null && ModelState.IsValid)
            {
                position.LastUpdated = DateTime.UtcNow; //sets current time at update
                dbContext.Positions.Update(position);
                await dbContext.SaveChangesAsync();
            }

            return Json(new[] { position }.ToDataSourceResult(request, ModelState));
        }

        public async Task<IActionResult> Delete([DataSourceRequest] DataSourceRequest request, Position position)
        {
            if (position != null && ModelState.IsValid)
            {
                dbContext.Positions.Remove(position);
                await dbContext.SaveChangesAsync();
            }

            return Json(new[] { position }.ToDataSourceResult(request, ModelState));
        }

    }
}
