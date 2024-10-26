using ASPGTRTraining.Model.DTO;
using ASPGTRTraining.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPGTRTraining.DataAccess.Repositories.Interface
{
    public interface IEmployeeRepo:IRepository<Employee>
    {
        Task<List<EmpListDTO>> GetIncludeDept();
    }
}
