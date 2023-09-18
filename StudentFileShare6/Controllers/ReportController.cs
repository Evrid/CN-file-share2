using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentFileShare6.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentFileShare6.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ReportContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ReportsController(ReportContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Reports
        public async Task<IActionResult> Index()
        {
            return View(await _context.Reports.ToListAsync());
        }

        // GET: Reports/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var report = await _context.Reports.FindAsync(id);
            if (report == null)
            {
                return NotFound();
            }
            return View(report);
        }

        // GET: Reports/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string DocumentID, int ReportType)
        {
            // Your logic to create a report as in the original PostReport method

            Report report = new Report();

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
            report.DocumentID = DocumentID;
            report.ReportType= ReportType; 

            // If the user is logged in, save their ID.
            if (userId != null)
            {
                report.UserID = userId;
            }
            // Else, leave report.UserID as it is


            _context.Reports.Add(report);
            _context.SaveChanges();

           
            return View();
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



        // GET: Reports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Reports.FindAsync(id);
            if (report == null)
            {
                return NotFound();
            }
            return View(report);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReportID, DocumentID, ReportType")] Report report)
        {
            // Logic for editing, similar to the original PutReport method

            if (id != report.ReportID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(report);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(report);
        }

        // GET: Reports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Reports
                .FirstOrDefaultAsync(m => m.ReportID == id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Logic for deleting similar to original DeleteReport method

            var report = await _context.Reports.FindAsync(id);
            _context.Reports.Remove(report);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
