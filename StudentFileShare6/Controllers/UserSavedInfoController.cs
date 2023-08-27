﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentFileShare6.data;
using StudentFileShare6.Models;
using System.Linq;
using System.Threading.Tasks;

namespace StudentFileShare6.Controllers
{
    public class UserSavedInfoController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly UserSavedInfoContext _context;
        private readonly CourseContext _courseContext;

        public UserSavedInfoController(UserManager<IdentityUser> userManager, UserSavedInfoContext context, CourseContext courseContext)
        {
            _userManager = userManager;
            _context = context;
            _courseContext = courseContext;
        }

        //public async Task<IActionResult> UserSaved()
        //{
        //    var userId = _userManager.GetUserId(User);  // Get current logged-in user's id

        //    var userSavedInfo = await _context.UserSavedInfo
        //        .Where(u => u.UserId == userId)
        //        .Include(u => u.Course)
        //        .Include(u => u.Document)
        //        .Include(u => u.University)
        //        .ToListAsync();

        //    return View(userSavedInfo);
        //}


        [HttpPost]
        public async Task<IActionResult> SaveCourse(string courseId)
        {
            var userId = _userManager.GetUserId(User);  // Get current logged-in user's id

            // First, check if the course is already saved by the user
            var existingSavedCourse = await _context.UserSavedInfo
                .Where(u => u.UserId == userId && u.CourseID == courseId)
                .FirstOrDefaultAsync();

            if (existingSavedCourse != null)
            {
                // Course is already saved
                return Json(new { success = false, message = "Course already saved" });
            }

            var courseToSave = await _courseContext.Course
                .Where(c => c.CourseID == courseId)
                .FirstOrDefaultAsync();

            if (courseToSave == null)
            {
                // Course not found
                return Json(new { success = false,  message = "Course not found" });
            }

            var userSavedInfo = new UserSavedInfo
            {
                UserId = userId,
                CourseID = courseToSave.CourseID,
                CourseName = courseToSave.CourseName
            };

            _context.UserSavedInfo.Add(userSavedInfo);
            await _context.SaveChangesAsync();

            return Json(new { success = false, message = "Course saved" });
        }

    }
}