using FinanceTracker.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

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
                UserId = "ce9dc970-8ba1-4aee-af0f-6082a244800a",
            };

            var workshift1 = new WorkShift
            {
                StartTime = new DateTime(2025, 4, 11, 9, 0, 0),
                EndTime = new DateTime(2025, 4, 11, 17, 0, 0),
                UserId = "ce9dc970-8ba1-4aee-af0f-6082a244800a",
            };

            var job = new Job
            {
                CVR = 1,
                HourlyRate = 150,
                UserId = "ce9dc970-8ba1-4aee-af0f-6082a244800a"
            };

            context.WorkShifts.Add(workshift);
            context.WorkShifts.Add(workshift1);
            context.Jobs.Add(job);

            context.SaveChanges();
        }
    }
}
