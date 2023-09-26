using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassData.Domain.Entity
{
    public class Salary
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string EmployeeName { get; set; }

        [Required]
        public double SalaryAmount { get; set; }
        public string SalaryStatus { get; set; }
        public DateTime SalaryReceivingDate { get; set; }
    }
}
