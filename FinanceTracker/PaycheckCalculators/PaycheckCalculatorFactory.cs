using FinanceTracker.PaycheckCalculators.ConcretePaycheckCalculators;

namespace FinanceTracker.PaycheckCalculators
{
    public class PaycheckCalculatorFactory
    {


        public PaycheckCalculator CreatePayCheckCalculator(int age, string taxcard /*and more*/)
        {
            PaycheckCalculator paycheckCalculator = null;
            if (age < 18) paycheckCalculator = new TeenPaycheckCalculator();


            return paycheckCalculator;
        }







    }
}
