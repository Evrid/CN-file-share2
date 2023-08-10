




using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;

namespace StudentFileShare6.Models
{
    public class Course
    {
        public string CourseID { get; set; }
        public string CourseName { get; set; }
        public string SchoolID { get; set; }
       public University? Universities { get; set; }   //table called "universities" in SQL as an object of class "university"

        public List<Document>? Documents { get; set; }   //nullable

        // public void GenerateRandomCourseID(DbContextOptions<CourseContext> options)
        public void GenerateRandomCourseID(CourseContext context)
        {
            var random = new Random();
            string generatedCourseID;

            do
            {
                generatedCourseID = $"{random.Next(1000, 9999)}{SchoolID}";
                //generate a random 4 digit number and add SchoolID for courseID
            } while (!IsCourseIDUnique(context, generatedCourseID));

            CourseID = generatedCourseID;
        }

        private bool IsCourseIDUnique(CourseContext context, string courseID)
        {
            
                return !context.Course.Any(c => c.CourseID == courseID);
           
        }
    }
}






