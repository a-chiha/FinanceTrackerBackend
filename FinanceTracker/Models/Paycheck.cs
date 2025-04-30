using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceTracker.Models
{
    public class Paycheck
    {
        public int PaycheckId { get; set; } 
        public decimal TaxSum { get; set; } // Tax 

        public decimal Tax { get; set; } 
        public decimal GrossSalary { get; set; } // SalarayBeforeTax
 
        public decimal NetSalary { get; set; } // SalaryAfterTax

        public decimal AMContribution { get; set; } 
        public TimeSpan WorkedHours { get; set; } // Dynamic
    }
}
