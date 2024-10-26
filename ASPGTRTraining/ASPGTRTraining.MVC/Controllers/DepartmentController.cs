using ASPGTRTraining.DataAccess.Repositories;
using ASPGTRTraining.Model.DTO;
using ASPGTRTraining.Model.Entity;
using Microsoft.AspNetCore.Mvc;

namespace ASPGTRTraining.MVC.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public DepartmentController(IUnitOfWork unitOfWork)
        {
            unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(string Id)
        {
            var departments = await unitOfWork.DepartmentRepo.GetAll();

            if (!string.IsNullOrEmpty(Id))
            {
                departments = departments.Where(d => d.Id == Id).ToList();
            }

            return View(departments);
        }

        public async Task<IActionResult> TotalDepartmentCount()
        {
            var departments = await unitOfWork.DepartmentRepo.GetAll();
            var total = departments.Count;
            return Ok(total);
        }

        public async Task<IActionResult> GetByID(string Id)
        {
            var department = await unitOfWork.DepartmentRepo.GetById(Id);
            if (department is null)
            {
                return NotFound();
            }
            return Ok(department);
        }

        [HttpGet]
        public async Task<IActionResult> DepartmentSave(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                return View();
            }

            var department = await unitOfWork.DepartmentRepo.GetById(Id);
            if (department is null)
            {
                return NotFound();
            }

            return View(department);
        }

        [HttpPost]
        public async Task<IActionResult> DepartmentSave(DeptListDTO model)
        {
            if (!string.IsNullOrEmpty(model.Id))
            {
                var department = await unitOfWork.DepartmentRepo.GetById(model.Id);
                if (department is null)
                {
                    return NotFound();
                }

                department.Name = model.Name;
                unitOfWork.DepartmentRepo.Edit(department);
                await unitOfWork.DepartmentRepo.Save();
            }
            else
            {
                var newDepartment = new Department
                {
                    Name = model.Name
                };
                unitOfWork.DepartmentRepo.Add(newDepartment);
                await unitOfWork.DepartmentRepo.Save();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> DepartmentDelete(string Id)
        {
            var department = await unitOfWork.DepartmentRepo.GetById(Id);
            if (department is null)
            {
                return NotFound();
            }

            unitOfWork.DepartmentRepo.Delete(department);
            await   unitOfWork.DepartmentRepo.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}