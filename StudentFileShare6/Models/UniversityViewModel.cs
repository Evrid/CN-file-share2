namespace StudentFileShare6.Models
{
    public class UniversityViewModel
    {

        public string SchoolID { get; set; }
        public string Name { get; set; }

        public string Location { get; set; }
        public List<Course>? Courses { get; set; }  //list of courses offered by a school
    }
}
