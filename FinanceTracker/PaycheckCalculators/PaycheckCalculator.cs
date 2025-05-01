using FinanceTracker.Models;

namespace FinanceTracker.PaycheckCalculators
{
    public class PaycheckCalculator
    {
        private Paycheck _paycheck;
        private List<WorkShift> _workshiftsInMonth;


        public Paycheck GeneratePaycheck(List<WorkShift> workShifts)
        {
            _workshiftsInMonth = workShifts;

            CalculateBaseSalary();
            CalculatéSalaryAfterTax();
            CalculatePension();

            return _paycheck;
        }




        protected void CalculateBaseSalary()
        {
        }

        protected void CalculatéSalaryAfterTax()
        {

        }

        protected void CalculatePension()
        {

        }







    }
}
