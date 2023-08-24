namespace StudentFileShare6.Models
{
    public class CourseViewModel
    {
        public string SchoolName { get; set; }
        public string CourseName { get; set; }

        public string CourseID { get; set; }

        public List<Document> Documents { get; set; }
    }
}
