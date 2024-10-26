using ASPGTRTraining.DataAccess.Repositories;
using ASPGTRTraining.Model.DTO;
using ASPGTRTraining.Model.Entity;
using Microsoft.AspNetCore.Mvc;

namespace ASPGTRTraining.MVC.Controllers
{
    public class DesignationController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public DesignationController(IUnitOfWork unitofWork)
        {
            unitOfWork = unitofWork;
        }

        public async Task<IActionResult> Designation(string Id)
        {
            var designations = await unitOfWork.DesignationRepo.GetAll();

            if (!string.IsNullOrEmpty(Id))
            {
                designations = designations.Where(d => d.Id == Id).ToList();
            }

            return View(designations);
        }

        public async Task<IActionResult> TotalDesignationCount()
        {
            var designations = await unitOfWork.DesignationRepo.GetAll();
            var total = designations.Count;
            return Ok(total);
        }

        public async Task<IActionResult> GetById(string Id)
        {
            var designation = await unitOfWork.DesignationRepo.GetById(Id);
            if (designation is null)
            {
                return NotFound();
            }
            return Ok(designation);
        }

        [HttpGet]
        public async Task<IActionResult> DesignationSave(string Id)
        {
            if (string.IsNullOrEmpty(Id))
            {
                return View();
            }

            var designation = await unitOfWork.DesignationRepo.GetById(Id);
            if (designation is null)
            {
                return NotFound();
            }

            return View(designation);
        }

        [HttpPost]
        public async Task<IActionResult> DesignationSave(DesigListDTO model)
        {
            if (!string.IsNullOrEmpty(model.Id))
            {
                var designation = await unitOfWork.DesignationRepo.GetById(model.Id);
                if (designation is null)
                {
                    return NotFound();
                }

                designation.Name = model.Name;
                unitOfWork.DesignationRepo.Edit(designation);
                await unitOfWork.DesignationRepo.Save();
            }
            else
            {
                var newDesignation = new Designation
                {
                    Name = model.Name
                };
                unitOfWork.DesignationRepo.Add(newDesignation);
                await unitOfWork.DesignationRepo.Save();
            }

            return RedirectToAction(nameof(Designation));
        }

        [HttpGet]
        public async Task<IActionResult> DesignationDelete(string Id)
        {
            var designation = await unitOfWork.DesignationRepo.GetById(Id);
            if (designation is null)
            {
                return NotFound();
            }

            unitOfWork.DesignationRepo.Delete(designation);
            await unitOfWork.DesignationRepo.Save();

            return RedirectToAction(nameof(Designation));
        }
    }
}