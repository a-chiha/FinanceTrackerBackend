using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceTracker.Models
{
    public class Paycheck
    {
        public int PaycheckId { get; set; }

        public decimal SalaryBeforeTax { get; set; }
        public TimeSpan WorkedHours { get; set; }
        public decimal AMContribution { get; set; }
        public decimal Tax { get; set; }
        public decimal SalaryAfterTax { get; set; }
        public decimal VacationPay { get; set; }

        [Range(1, 12, ErrorMessage = "Month must be between 1 and 12.")]
        public int Month { get; set; }
    }
}
