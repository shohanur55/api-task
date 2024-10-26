using ASPGTRTraining.DataAccess;
using ASPGTRTraining.Model.DTO;
using ASPGTRTraining.Model.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPGTRTraining.MVC.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ASPGTRTrainingDBContext db;

        public EmployeeController(ASPGTRTrainingDBContext db)
        {
            this.db = db;
        }

        public async Task<IActionResult> privacy()
        {
            var employees = await db.Employees.ToListAsync();
            return View(employees);
        }

        public async Task<IActionResult> TotalEmployeeCount()
        {
            var total = await db.Employees.CountAsync();
            return Ok(total);
        }

        public async Task<IActionResult> GetByEmpID(string Id)
        {
            var employee = await db.Employees.SingleOrDefaultAsync(e => e.Id == Id);
            if (employee is null) return NotFound();
            return Ok(employee);
        }

        [HttpGet]
        public async Task<IActionResult> EmployeeSave(string Id)
        {
            ViewBag.Departments = await db.Departments.ToListAsync();
            ViewBag.Designations = await db.Designations.ToListAsync();

            if (string.IsNullOrEmpty(Id)) return View();

            var employee = await db.Employees.SingleOrDefaultAsync(e => e.Id == Id);
            if (employee is null) return NotFound();

            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> EmployeeSave(EmpListDTO model)
        {
            // Verify department existence
            if (!await db.Departments.AnyAsync(d => d.Id == model.DeptId))
            {
                ModelState.AddModelError("DeptId", "The selected department does not exist.");
                return View(model);
            }

            // Verify designation existence if applicable
            if (!string.IsNullOrEmpty(model.DesigID) &&
                !await db.Designations.AnyAsync(d => d.Id == model.DesigID))
            {
                ModelState.AddModelError("DesignationId", "The selected designation does not exist.");
                return View(model);
            }

            if (!string.IsNullOrEmpty(model.Id))
            {
                var employee = await db.Employees.SingleOrDefaultAsync(e => e.Id == model.Id);
                if (employee is null) return NotFound();

                employee.Name = model.Name;
                employee.Address = model.Address;
                employee.City = model.City;
                employee.Phone = model.Phone;
                employee.DeptId = model.DeptId;
                employee.DesigID = model.DesigID;
                await db.SaveChangesAsync();
            }
            else
            {
                var newEmployee = new Employee
                {
                    Name = model.Name,
                    Address = model.Address,
                    City = model.City,
                    Phone = model.Phone,
                    DeptId = model.DeptId,
                    DesigID = model.DesigID
                };
                await db.AddAsync(newEmployee);
                await db.SaveChangesAsync();
            }

            return RedirectToAction(nameof(privacy));
        }


        [HttpGet]
        public async Task<IActionResult> EmployeeDelete(string Id)
        {
            var employee = await db.Employees.SingleOrDefaultAsync(e => e.Id == Id);
            if (employee is null) return NotFound();

            db.Employees.Remove(employee);
            await db.SaveChangesAsync();

            return RedirectToAction(nameof(privacy));
        }
    }
}
