namespace StudentFileShare6.Models
{
    public class UserSavedInfo
    {
        public int Id { get; set; }
        public string UserId { get; set; }   //you must login to save

        public string? CourseName { get; set; }
        public string? CourseID { get; set; }

        public string? DocumentName { get; set; }
        public string? DocumentID { get; set; }

        public string? SchoolName { get; set; }
        public string? SchoolID { get; set; }
        // additional properties here

        public Course? Course { get; set; }
        public Document? Document { get; set; }
        public University? University { get; set; }
    }

}
