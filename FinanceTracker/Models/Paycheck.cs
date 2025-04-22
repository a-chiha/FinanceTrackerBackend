using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceTracker.Models
{
    public class Paycheck
    {
        [Key]
        public DateOnly SalaryPeriod { get; set; } // Dynamic


        public decimal Tax { get; set; }
        public decimal SalarayBeforeTax { get; set; } // Dynamic
        public decimal HolidaySupplement { get; set; }
        public decimal Pension { get; set; } 
        public decimal Holidaycompensation { get; set; }

        public decimal taxDeduction { get; set; } // Dynamic

        public decimal AMContribution { get; set; }
        public TimeOnly WorkedHours { get; set; } // Dynamic

        public ICollection<WorkShift> WorkShifts { get; set; } // Dynamic ??

    }
}
