using ASPGTRTraining.DataAccess.Repositories.Interface;
using ASPGTRTraining.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPGTRTraining.DataAccess.Repositories.implement
{
    public class DesignationRepo : Repository<Designation>, IDesignationRepo
    {
        public DesignationRepo(ASPGTRTrainingDBContext db) : base(db)
        {

        }
    }
}
