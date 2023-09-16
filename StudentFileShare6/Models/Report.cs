
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentFileShare6.Models
{
    public class Report
    {
  
        public int ReportID { get; set; }

       
        public int DocumentID { get; set; }
        //public virtual Document Document { get; set; } // Navigation property

        public Document? document { get; set; }

       
        public int UserID { get; set; }
       // public virtual User User { get; set; } // Navigation property

        public int ReportType { get; set; }

        [MaxLength(500)] // Adjust as needed
        public string ReportDescription { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime ReportDate { get; set; }

        [MaxLength(100)] // Adjust as needed
        public string HandledBy { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ResolutionDate { get; set; } // Nullable because it may not be resolved immediately

        public int ResolutionType { get; set; }
    }
}
