using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPGTRTraining.DataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ASPGTRTrainingDBContext db;
        public Repository(ASPGTRTrainingDBContext db) {
        this.db = db;
        }
        public void Add(T model)
        {
           db.Set<T>().Add(model);
        }

        public void Delete(T model)
        {
            db.Set<T>().Remove(model);
        }

        public void Edit(T model)
        {
            db.Set<T>().Attach(model);
            db.Entry(model).State = EntityState.Modified;

        }

        public async Task<List<T>> GetAll()
        {
            return await db.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(object id)
        {
            return await db.Set<T>().FindAsync(id);
        }

        public async Task Save()
        {
            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving changes: {ex.Message}");
                throw; // Re-throw to log elsewhere if needed
            }
        }

    }
}
