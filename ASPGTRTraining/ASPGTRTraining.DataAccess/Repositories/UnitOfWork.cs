using ASPGTRTraining.DataAccess.Repositories.implement;
using ASPGTRTraining.DataAccess.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPGTRTraining.DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IDepartmentRepo DepartmentRepo { get; private set; }

        public IDesignationRepo DesignationRepo { get; private set; }


        public IEmployeeRepo EmployeeRepo { get; private set; }


        public UnitOfWork(ASPGTRTrainingDBContext db)
        { 
            DepartmentRepo = new DepartmentRepo(db);
            DesignationRepo = new DesignationRepo(db);
            EmployeeRepo = new EmployeeRepo(db);


        }

    }
}
