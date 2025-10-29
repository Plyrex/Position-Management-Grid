using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Technical_Exam.Domain.Entities;
using Technical_Exam.Domain.Enums;
using Technical_Exam.Infrastructure.Data;
using Technical_Exam.Infrastructure.Models;

namespace Technical_Exam.Web.Controllers
{
    public class PositionsController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public PositionsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //get all positions (READ)
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var positions = await dbContext.Positions.ToListAsync();
            return View(positions);
        }

        //get the add position view
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        //post a new position to the database (CREATE)
        [HttpPost]
        public async Task<IActionResult> Add(CreatePositionViewModel viewModel)
        {
            var position = new Position
            {
                PositionName= viewModel.PositionName,
                DepartmentId=viewModel.DepartmentId,
                LocationId=viewModel.LocationId,
                JobId=viewModel.JobId,
                EmploymentType=viewModel.EmploymentType,
                EmploymentStatus=viewModel.EmploymentStatus,
                EmployeeId=viewModel.EmployeeId,
                CreatedById=viewModel.CreatedById,
                TimeCreated= DateTime.UtcNow
            };

            await dbContext.Positions.AddAsync(position);
            await dbContext.SaveChangesAsync();

            return View();
        }

        //get the edit position view
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var position= await dbContext.Positions.FindAsync(id);

            return View(position);
        }

        //update a position in the database (UPDATE)
        [HttpPost]
        public async Task<IActionResult> Edit(Position viewModel)
        {
            var position= await dbContext.Positions.FindAsync(viewModel.PositionId);
            if(position is not null)
            {
                position.PositionName = viewModel.PositionName;
                position.DepartmentId = viewModel.DepartmentId;
                position.LocationId = viewModel.LocationId;
                position.JobId = viewModel.JobId;
                position.EmploymentType = viewModel.EmploymentType;
                position.EmploymentStatus = viewModel.EmploymentStatus;
                position.EmployeeId = viewModel.EmployeeId;
                position.UpdatedById = viewModel.UpdatedById;
                position.LastUpdated = DateTime.UtcNow;

                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Positions");
        }

        //delete a postion (DELETE)
        [HttpPost]
        public async Task<IActionResult> Delete(Position viewModel)
        {
            var position= await dbContext.Positions.AsNoTracking().FirstOrDefaultAsync(x => x.PositionId== viewModel.PositionId);
            if (position is not null)
            {
                dbContext.Positions.Remove(viewModel);
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Positions");
        }

    }
}
