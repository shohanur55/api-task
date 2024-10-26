using ASPGTRTraining.DataAccess.Repositories.Interface;
using ASPGTRTraining.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPGTRTraining.DataAccess.Repositories.implement
{
    public class DepartmentRepo : Repository<Department>, IDepartmentRepo
    {
        public DepartmentRepo(ASPGTRTrainingDBContext db) : base(db)
        {

        }
    }
}
