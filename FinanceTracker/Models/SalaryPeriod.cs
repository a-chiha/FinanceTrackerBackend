using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.Models
{
    public class SalaryPeriod
    {
        [Key]
        public int SalaryPeriodId { get; set; }
        public DateOnly SalaryStartDate { get; set; } // static
        public DateOnly SarlaryEndDate { get; set; } // static
    }
}
