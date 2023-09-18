using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentFileShare6.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;


namespace StudentFileShare6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly ReportContext _context; // Assuming you have a DbContext named YourDbContext
    
        private readonly UserManager<IdentityUser> _userManager;

        public ReportsController(ReportContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Reports
        [HttpGet]
        public ActionResult<IEnumerable<Report>> GetReports()
        {
            return _context.Reports.ToList();
        }

        // GET: api/Reports/5
        [HttpGet("{id}")]
        public ActionResult<Report> GetReport(int id)
        {
            var report = _context.Reports.Find(id);
            if (report == null)
            {
                return NotFound();
            }
            return report;
        }

        // POST: api/Reports
        [HttpPost]
        public ActionResult<Report> PostReport(string DocumentID, int ReportType)
        {
            Report report=new Report();

            // Get the ID of the currently logged-in user
            var userId = _userManager.GetUserId(User);


            int newReportID;

            // Ensure a unique 10-digit ReportID is generated.
            do
            {
                newReportID = GenerateRandom10DigitNumber();
            } while (_context.Reports.Any(r => r.ReportID == newReportID));

            report.ReportID = newReportID;
            report.ReportDate = DateTime.Now;  // Setting the ReportDate to the current date and time


            // If the user is logged in, save their ID.
            if (userId != null)
            {
                report.UserID = userId;
            }
            // Else, leave report.UserID as it is


            _context.Reports.Add(report);
            _context.SaveChanges();

            return CreatedAtAction("GetReport", new { id = report.ReportID }, report);
        }

        /// <summary>
        /// Generates a random 10-digit integer.
        /// </summary>
        /// <returns>A random 10-digit integer.</returns>
        private int GenerateRandom10DigitNumber()
        {
            // Note: This method assumes that int has at least 10 digits, which is true for .NET's 32-bit int.
            // A 10-digit number ranges from 1,000,000,000 to 9,999,999,999. 
            // However, the upper limit for int is 2,147,483,647, so we need to adjust accordingly.

            Random random = new Random();
            int lowerBound = 1000000000;
            int upperBound = 2147483647; // int.MaxValue in .NET is 2,147,483,647
            return random.Next(lowerBound, upperBound);
        }


        // PUT: api/Reports/5
        [HttpPut("{id}")]
        public IActionResult PutReport(int id, Report report)
        {
            if (id != report.ReportID)
            {
                return BadRequest();
            }

            _context.Entry(report).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/Reports/5
        [HttpDelete("{id}")]
        public IActionResult DeleteReport(int id)
        {
            var report = _context.Reports.Find(id);
            if (report == null)
            {
                return NotFound();
            }

            _context.Reports.Remove(report);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
