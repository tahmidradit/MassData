using MassData.Domain.Entity;
using MassData.Repository.Data;
using MassData.Repository.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassData.Service.Services
{
    public class SalaryService : ISalary
    {
        private readonly ApplicationDbContext context;

        public SalaryService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Salary> AddSalary(Salary salary)
        {
            await context.Salaries.AddAsync(salary);
            await context.SaveChangesAsync();
            return salary;
        }

        public async Task<Salary> DeleteSalary(int id)
        {
            var findById = await context.Salaries.FirstOrDefaultAsync(x => x.Id == id);

            if (findById == null)
            {
                throw new Exception("Salary Information Not Found");
            }

            context.Remove(findById);
            await context.SaveChangesAsync();

            return findById;
        }

        public async Task<Salary> EditSalary(Salary salary)
        {
            var findById = await context.Salaries.FirstOrDefaultAsync(x => x.Id == salary.Id);

            if (findById == null)
            {
                throw new Exception("Salary Information Not Found");
            }

            context.Update(findById);
            await context.SaveChangesAsync();

            return salary;
        }

        public async Task<IEnumerable<Salary>> GetAll()
        {
            var salaries = await context.Salaries.ToListAsync();
            return salaries;
        }

        public async Task<Salary> GetById(int id)
        {
            var findById = await context.Salaries.FirstOrDefaultAsync(x => x.Id == id);

            if (findById == null)
            {
                throw new Exception("Salary Information Not Found");
            }

            return findById;
        }
    }
}
