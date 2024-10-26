using ASPGTRTraining.Model.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPGTRTraining.DataAccess
{
    public class ASPGTRTrainingDBContext : DbContext
    {
        public ASPGTRTrainingDBContext(DbContextOptions <ASPGTRTrainingDBContext> options) : base(options)
        {
        }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Designation> Designations { get; set; }
    }
}
