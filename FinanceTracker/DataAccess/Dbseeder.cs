using FinanceTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.DataAccess
{
    public class Dbseeder
    {

        public static void Initialize(FinanceTrackerContext context)
        {

            var workshift = new WorkShift
            {
                StartTime = new DateTime(2025, 4, 10, 9, 0, 0),
                EndTime = new DateTime(2025, 4, 10, 17, 0, 0),
                UserId = "5aa9bf7e-2963-4284-8b5f-bd2ac34738f5",
            };
            var workshift1 = new WorkShift
            {
                StartTime = new DateTime(2025, 4, 11, 9, 0, 0),
                EndTime = new DateTime(2025, 4, 11, 17, 0, 0),
                UserId = "5aa9bf7e-2963-4284-8b5f-bd2ac34738f5",
            };
            var workshift2 = new WorkShift
            {
                StartTime = new DateTime(2025, 4, 12, 9, 0, 0),
                EndTime = new DateTime(2025, 4, 12, 17, 0, 0),
                UserId = "5aa9bf7e-2963-4284-8b5f-bd2ac34738f5",
            };
            var workshift3 = new WorkShift
            {
                StartTime = new DateTime(2025, 4, 13, 9, 0, 0),
                EndTime = new DateTime(2025, 4, 13, 17, 0, 0),
                UserId = "5aa9bf7e-2963-4284-8b5f-bd2ac34738f5",
            };
            var job = new Job
            {
                CVR = 1,
                HourlyRate = 150,
                UserId = "5aa9bf7e-2963-4284-8b5f-bd2ac34738f5"
            };

            context.WorkShifts.Add(workshift);
            context.WorkShifts.Add(workshift1);
            context.WorkShifts.Add(workshift2);
            context.WorkShifts.Add(workshift3);
            context.Jobs.Add(job);
            context.SaveChanges();

        }
    }
}
