using FinanceTracker.Models;

namespace FinanceTracker.DTO
{
    public class PaycheckDTO
    {
        public SalaryPeriod PaycheckPeriod { get; set; } // static

        public string Tax { get; set; } // static
        public decimal SalarayBeforeTax { get; set; } // Dymaic

        // public decimal HolidaySupplement { get; set; } 
        // public decimal Pension { get; set; }

        // public decimal Holidaycompensation { get; set; }

        public decimal taxDeduction { get; set; } // Dynamic 

        public decimal AMContribution { get; set; } // Static
        public TimeOnly WorkedHours { get; set; } // Dynamic
    }
}
