using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore;
using StudentFileShare6.data;
using StudentFileShare6.Models;

namespace StudentFileShare6.Controllers
{
    public class SearchController : Controller
    {
        private readonly DocumentContext _context;

        private readonly UniversityContext _universityContext;
        private readonly CourseContext _courseContext;

        public SearchController(DocumentContext context, CourseContext courseContext, UniversityContext universityContext)
        {
            _context = context;
            _courseContext = courseContext;
            _universityContext = universityContext;
        }


        public IActionResult Index()
        {
            return View();
        }



        public IActionResult Search(string SchoolID, string CourseID, string textInput)
        //we can use form in cshtml specify this method, so it is not called directly, for example
        //<form id="searchDocumentForm" asp-action="Search" enctype="multipart/form-data">
        //but if we want to update dynamically each time we click then we cannot use this, we need to use javascript etc
        {


            //    When using var, we declare implicit type variables, the compiler infers the variable’s type from its value automatically

            var searchResults = _context.Document
    .Where(d => d.Name.Contains(textInput) && d.SchoolID == SchoolID && d.CourseID == CourseID)
    .Select(d => new
    {
        d.Name,
        d.DocumentID,
        CourseName = d.Course.CourseName, // Include only CourseName and assign it to Name property
        UniversityName=d.University.Name,   //cannot use     .Include(d => d.University)    because won't work in Json
        d.SchoolID,
        d.CourseID,
        d.Year,
        d.Category,
        d.Description,
        d.Rating,

        d.Anonymous,
        d.UserID,
        d.Link,
        d.FirstPageImageLink,
        d.University
    })
    .ToList();


            return Json(searchResults);
        }

       





    }
}
