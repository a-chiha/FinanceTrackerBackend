//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace FinanceTracker.Models
//{
//    public class PaycheckInfo
//    {
//        [Key]
//        public int Id { get; set; } // static

//        [Range(0.0, 1.0, ErrorMessage = "Tax must be between 0% and 100% (i.e., 0.0 to 1.0)")]
//        public decimal Tax { get; set; } // static

//        // public decimal HolidaySupplement { get; set; } 
//        // public decimal Pension { get; set; }
//        // public decimal Holidaycompensation { get; set; }

//        // public decimal AMContribution { get; set; } // Static



//        public ICollection<WorkShift> WorkShifts { get; set; }
//    }
//}

