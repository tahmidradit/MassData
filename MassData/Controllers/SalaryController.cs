using MassData.Domain.Entity;
using MassData.Repository.Data;
using MassData.Repository.Repositories;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MassData.Controllers
{
    public class SalaryController : Controller
    {
        private readonly ISalary salary;
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment webHostingEnvironment;

        public SalaryController(ISalary salary,ApplicationDbContext context,IWebHostEnvironment webHostingEnvironment)
        {
            this.salary = salary;
            this.context = context;
            this.webHostingEnvironment = webHostingEnvironment;
        }
         
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var salaries = await this.salary.GetAll();
            return View(salaries);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, ActionName("Create"), ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(Salary salary)
        {
            await this.salary.AddSalary(salary);

            //File upload

            string webRootPath = webHostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            var findProductsById = await context.Salaries.FindAsync(salary.Id);

            if (files.Count > 0)
            {
                //files has been uploaded
                var uploads = Path.Combine(webRootPath, "images");
                var extension = Path.GetExtension(files[0].FileName);

                using (var filesStream = new FileStream(Path.Combine(uploads, salary.Id + extension), FileMode.Create))
                {
                    files[0].CopyTo(filesStream);
                }
                
            }
            else
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var findById = await context.Salaries.SingleOrDefaultAsync(m => m.Id == id);

            if (findById == null)
            {
                return NotFound();
            }

            return View(findById);
        }

        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> Edit(Salary salaryModel)
        {
            await salary.EditSalary(salaryModel);

            var findById = await context.Salaries.FirstOrDefaultAsync(x => x.Id == salaryModel.Id);

            if (findById == null)
            {
                return NotFound();
            }

            //File upload

            string webRootPath = webHostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            

            if (files.Count > 0)
            {
                //files has been uploaded
                var uploads = Path.Combine(webRootPath, "images");
                var extension = Path.GetExtension(files[0].FileName);

                using (var filesStream = new FileStream(Path.Combine(uploads, findById.Id + extension), FileMode.Create))
                {
                    files[0].CopyTo(filesStream);
                }

            }
            else
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var findById = await context.Salaries.SingleOrDefaultAsync(m => m.Id == id);

            if (findById == null)
            {
                return NotFound();
            }

            return View(findById);
        }

        [HttpPost, ActionName("Delete")]
        
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            string webRootPath = webHostingEnvironment.WebRootPath;
            var findById = await context.Salaries.FindAsync(id);

            if (findById != null)
            {
                var imagePath = Path.Combine(webRootPath);

                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
                
                await context.SaveChangesAsync();

            }
            await salary.DeleteSalary(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
