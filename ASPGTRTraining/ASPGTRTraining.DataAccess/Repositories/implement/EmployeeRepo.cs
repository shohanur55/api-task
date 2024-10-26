using ASPGTRTraining.DataAccess.Repositories.Interface;
using ASPGTRTraining.Model.DTO;
using ASPGTRTraining.Model.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPGTRTraining.DataAccess.Repositories.implement
{
    public class EmployeeRepo : Repository<Employee>, IEmployeeRepo
    {
        public EmployeeRepo(ASPGTRTrainingDBContext db) : base(db)
        {
        }

        public async Task<List<EmpListDTO>> GetIncludeDept()
        {
            return await db.Employees.Include(d => d.Dept)
                .Select(x=> new EmpListDTO{
                    Address = x.Address,
                    DeptId = x.DeptId,
                    Name = x.Name,
                    City = x.City,
                    Phone = x.Phone
            }).ToListAsync();
        }
    }
}
