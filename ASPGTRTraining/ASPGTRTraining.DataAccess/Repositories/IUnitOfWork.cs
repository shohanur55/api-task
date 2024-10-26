using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASPGTRTraining.DataAccess.Repositories.Interface;

namespace ASPGTRTraining.DataAccess.Repositories
{
    public interface IUnitOfWork
    {
        public IDepartmentRepo DepartmentRepo { get; }

        public IEmployeeRepo EmployeeRepo { get; }

        public IDesignationRepo DesignationRepo { get; }

    }
}
