using MassData.Domain.Entity;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassData.Repository.Repositories
{
    public interface ISalary
    {
        Task<IEnumerable<Salary>> GetAll();
        Task<Salary> GetById(int id);
        Task<Salary> AddSalary(Salary salary);
        Task<Salary> EditSalary(int id, Salary salary);
        Task<Salary> DeleteSalary(int id);
    }
}
