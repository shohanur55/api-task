    using ASPGTRTraining.DataAccess;
    using ASPGTRTraining.DataAccess.Repositories;
    using ASPGTRTraining.DataAccess.Repositories.Interface;
    using ASPGTRTraining.Model.DTO;
    using ASPGTRTraining.Model.Entity;
    using ASPGTRTraining.MVC.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Diagnostics;

    namespace ASPGTRTraining.MVC.Controllers
    {
        public class HomeController : Controller
        {
            private readonly ILogger<HomeController> _logger;
            private readonly ASPGTRTrainingDBContext db;
            private readonly IUnitOfWork unitOfWork;

            public HomeController(ILogger<HomeController> logger, ASPGTRTrainingDBContext db, IUnitOfWork unitOfWork)
            {
                _logger = logger;
                this.db = db;
                this.unitOfWork = unitOfWork;
            }

            public async Task<IActionResult> EmployeeList()
            {
                var emplist = await unitOfWork.EmployeeRepo.GetIncludeDept();
                return Ok(emplist);
            }

            public async Task<IActionResult> Index(string Id, String name)
            {
               
                //if (Id!=0)
                //{
                //    Deptlist = Deptlist.Where(x => x.Id.ToLower() == Id.ToString()).ToList();

                //}

                var query = db.Departments.AsQueryable();


                if (!string.IsNullOrEmpty(Id))
                {
                    query = query.Where(x => x.Id == Id);
                }

                var Deptlist = await query.ToListAsync();

                return View(Deptlist);
            }

            public async Task<IActionResult> TotalDeptlist()
            {
                var total =  await db.Departments.CountAsync(); 
                return Ok(total);
            }

            public async Task<IActionResult> GetByID(string Id)
            {
                var byidtotal = await db.Departments.Where(x=>x.Id==Id).SingleOrDefaultAsync();
                if (byidtotal is null)
                {
                    return NotFound();
                }
                return Ok(byidtotal);
            }

            public async Task<IActionResult> GetByIDview(string Id)
            {
                var total = await db.Departments.CountAsync();
                var byidtotal = await db.Departments.Where(x => x.Id == Id).SingleOrDefaultAsync();
                if (byidtotal is null)
                {
                    return NotFound();
                }
                ViewBag.Total = total;
                return View(byidtotal);
            }

            public async Task<IActionResult> Department2()
            {
                var Department = await db.Departments.Select(x => new DeptListDTO
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToListAsync();
                return Ok(Department);
            }

        [HttpGet]
        public async Task<IActionResult> DepartmentSave(string Id)
        {
            ViewBag.Departments = await unitOfWork.DepartmentRepo.GetAll();

            if (string.IsNullOrEmpty(Id))
            {
                return View();
            }

            var edit = await unitOfWork.DepartmentRepo.GetById(Id);
            if (edit is null)
            {
                return NotFound();
            }

            return View(edit);
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
                return RedirectToAction(nameof(Index));
            }

            var newDepartment = new Department()
            {
                Name = model.Name,
            };

            unitOfWork.DepartmentRepo.Add(newDepartment);
            await unitOfWork.DepartmentRepo.Save();

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
            await unitOfWork.DepartmentRepo.Save();

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Privacy(string Id, String name)
            {
                //var department = new Department()
                //{
                //    CreatedAt = DateTime.Now,
                //    Id = "1",
                //    Name = "Abdullah",
                //    IsDeleted = false,
                //    UpBy = "A",
                //};
                //var department1 = new Department()
                //{
                //    CreatedAt = DateTime.Now,
                //    Id = "2",
                //    Name = "Afnan",
                //    IsDeleted = false,
                //    UpBy = "A",
                //};
                //var department2 = new Department()
                //{
                //    CreatedAt = DateTime.Now,
                //    Id = "3",
                //    Name = "Jawad",
                //    IsDeleted = false,
                //    UpBy = "B",
                //};
                //var department3 = new Department()
                //{
                //    CreatedAt = DateTime.Now,
                //    Id = "4",
                //    Name = "Sayed",
                //    IsDeleted = false,
                //    UpBy = "A",
                //};
                //var department4 = new Department()
                //{
                //    CreatedAt = DateTime.Now,
                //    Id = "5",
                //    Name = "Rakib",
                //    IsDeleted = false,
                //    UpBy = "B",
                //};

                //List<Department> Deptlist = new List<Department>();
                //Deptlist.Add(department);
                //Deptlist.Add(department1);
                //Deptlist.Add(department2);
                //Deptlist.Add(department3);
                //Deptlist.Add(department4);

                //if (Id!=0)
                //{
                //    Deptlist = Deptlist.Where(x => x.Id.ToLower() == Id.ToString()).ToList();

                //}

                var query = db.Employees.AsQueryable();


                if (!string.IsNullOrEmpty(Id))
                {
                    query = query.Where(x => x.Id == Id);
                }

                var Emplist = await query.ToListAsync();

                return View(Emplist);
            }

            public async Task<IActionResult> TotalEmplist()
            {
                var total = await db.Employees.CountAsync();
                return Ok(total);
            }

            public async Task<IActionResult> GetByEmpID(string Id)
            {
                var empidtotal = await db.Employees.Where(e => e.Id == Id).SingleOrDefaultAsync();
                if (empidtotal is null)
                {
                    return NotFound();
                }
                return Ok(empidtotal);
            }

            public async Task<IActionResult> EmpByIDview(string Id)
            {
                var total = await db.Employees.CountAsync();
                var empidtotal = await db.Employees.Where(e => e.Id == Id).SingleOrDefaultAsync();
                if (empidtotal is null)
                {
                    return NotFound();
                }
                ViewBag.Total = total;
                return View(empidtotal);
            }

            public async Task<IActionResult> Employee2()
            {
                var Department = await db.Employees.Select(x => new EmpListDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address = x.Address,
                    City = x.City,
                    Phone = x.Phone
                }).ToListAsync();
                return Ok(Employee2);
            }

            [HttpGet]
            public async Task<IActionResult> EmployeeSave(string Id)
            {
                ViewBag.Departments = await db.Departments.ToListAsync(); 

                if (string.IsNullOrEmpty(Id))
                {
                    return View();
                }
                var edit = await db.Employees.Where(x => x.Id == Id).SingleOrDefaultAsync();
                if (edit is null)
                {
                    return NotFound();
                }

                return View(edit);
            }


            [HttpPost]
            public async Task<IActionResult> EmployeeSave(EmpListDTO model)
            {
            
                var departmentExists = await db.Departments.AnyAsync(d => d.Id == model.DeptId);
                if (!departmentExists)
                {
               
                    ModelState.AddModelError("DeptId", "The selected department does not exist.");
                    return View(model); 
                }

                if (!string.IsNullOrEmpty(model.Id))
                {
                
                    var edit = await db.Employees.Where(x => x.Id == model.Id).SingleOrDefaultAsync();
                    if (edit is null)
                    {
                        return NotFound();
                    }
                    edit.Name = model.Name;
                    edit.Address = model.Address;
                    edit.City = model.City;
                    edit.Phone = model.Phone;
                    edit.DeptId = model.DeptId;
                    await db.SaveChangesAsync();
                    return RedirectToAction(nameof(Privacy));
                }

                var employee = new Employee()
                {
                    Name = model.Name,
                    Address = model.Address,
                    City = model.City,
                    Phone = model.Phone,
                    DeptId = model.DeptId 
                };
                await db.AddAsync(employee);
                await db.SaveChangesAsync();

                return RedirectToAction(nameof(Privacy));
            }


            [HttpGet]
            public async Task<IActionResult> EmployeeDelete(string Id)
            {
                var edit = await db.Employees.Where(x => x.Id == Id).SingleOrDefaultAsync();
                if (edit is null)
                {
                    return NotFound();
                }

                db.Remove(edit);
                await db.SaveChangesAsync();

                return RedirectToAction(nameof(Privacy));

            }


            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
            public IActionResult Error()
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }
    }
