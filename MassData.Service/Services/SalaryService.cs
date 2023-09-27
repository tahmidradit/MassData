using Azure.Core;
using MassData.Domain.Entity;
using MassData.Repository.Data;
using MassData.Repository.Repositories;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        [BindProperty]
        Salary salaryModel { get; set; }

        private readonly ApplicationDbContext context;
        
        public SalaryService(ApplicationDbContext context)
        {
            this.context = context;
            salaryModel = new Salary();
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
        
        public async Task<Salary> EditSalary(int id,Salary salary)
        {
            


            context.Update(salary);
    
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
