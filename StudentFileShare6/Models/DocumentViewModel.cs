namespace StudentFileShare6.Models
{
    public class DocumentViewModel
    //use in DocumentController DocView() method, for individual page of each document
    {
        public string SchoolName { get; set; }
        public string CourseName { get; set; }
        public string DocumentName { get; set; }
        public string DocumentID { get; set; }
        public string Link { get; set; }
        public int? LikeNumber { get; set; }
        public int? DislikeNumber { get; set; }
        public float? Rating { get; set; }
    }

}
