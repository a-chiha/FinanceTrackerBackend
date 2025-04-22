using FinanceTracker.Models;
using FinanceTracker.Services.Interfaces;

namespace FinanceTracker.Services.Implementations
{
    public class PaycheckValidation : IPaycheckValidation
    {
        public void ValidateJob(Job job)
        {
            if (job == null)
                throw new ArgumentNullException(nameof(job));

            if (job.HourlyRate < 0)
                throw new ArgumentException("Hourly rate cannot be negative.");

            if (job.PaycheckInfo == null)
                throw new ArgumentException("Missing PaycheckInfo.");
        }

        public void ValidateWorkShift(WorkShift shift)
        {
            if (shift == null)
                throw new ArgumentNullException(nameof(shift));

            if (shift.EndTime < shift.StartTime)
                throw new ArgumentException("Work shift end time cannot be before start time.");

            if (shift.FinanceUserId <= 0)
                throw new ArgumentException("Invalid FinanceUserId in WorkShift.");
        }

        public void ValidatePaycheckInfo(PaycheckInfo info)
        {
            if (info == null)
                throw new ArgumentNullException(nameof(info));

            if (info.Tax < 0 || info.Tax > 1)
                throw new ArgumentException("Tax must be between 0 and 1 (0% - 100%).");

            if (info.WorkShifts == null || !info.WorkShifts.Any())
                throw new ArgumentException("PaycheckInfo must contain at least one WorkShift.");
        }
    }
}

