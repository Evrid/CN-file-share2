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
        private readonly UniversityContext _universityContext;
        private readonly DocumentContext _documentContext;

        public UserSavedInfoController(UserManager<IdentityUser> userManager, UserSavedInfoContext context, CourseContext courseContext, UniversityContext universityContext, DocumentContext documentContext)
        {
            _userManager = userManager;
            _context = context;
            _courseContext = courseContext;
            _universityContext = universityContext;
            _documentContext = documentContext;
        }

        public async Task<IActionResult> UserSaved()
        {
            var userId = _userManager.GetUserId(User);  // Get current logged-in user's id

            var userSavedInfo = await _context.UserSavedInfo
                .Where(u => u.UserId == userId)
                .Include(u => u.Course)
                .Include(u => u.Document)
                .Include(u => u.University)
                .ToListAsync();


         


            return View(userSavedInfo);
        }



        [HttpGet]
        public async Task<IActionResult> IsCourseFavorited(string courseId)
        {
            var userId = _userManager.GetUserId(User);  // Get current logged-in user's id

            // First, check if the course is already saved by the user
            var existingSavedCourse = await _context.UserSavedInfo
                .Where(u => u.UserId == userId && u.CourseID == courseId)
                .FirstOrDefaultAsync();

            if (existingSavedCourse != null)
            {
                // var isFavorited = true;
                return Json(new { isFavorited = true });
            }

            return Json(new { isFavorited = false });
        }

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
                // Course is already saved, so remove it
                _context.UserSavedInfo.Remove(existingSavedCourse);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "课程已移除收藏" ,isFavorited = false }); // course removed from saved
            }

            var courseToSave = await _courseContext.Course
                .Include(d => d.Universities)
                
                .Where(c => c.CourseID == courseId)
                .FirstOrDefaultAsync();

            if (courseToSave == null)
            {
                // Course not found
                return Json(new { success = false,  message = "找不到课程" });
            }

            var userSavedInfo = new UserSavedInfo
            {
                UserId = userId,
                CourseID = courseToSave.CourseID,
                CourseName = courseToSave.CourseName,
                SchoolName = courseToSave.Universities.Name
            };

            _context.UserSavedInfo.Add(userSavedInfo);
            await _context.SaveChangesAsync();

            return Json(new { success = false, message = "课程收藏成功" , isFavorited = true});
        }



        [HttpGet]
        public async Task<IActionResult> IsUniversityFavorited(string universityId)
        {
            var userId = _userManager.GetUserId(User);  // Get current logged-in user's id

            // First, check if the course is already saved by the user
            var existingSavedUniversity = await _context.UserSavedInfo
                .Where(u => u.UserId == userId && u.SchoolID == universityId)
                .FirstOrDefaultAsync();

            if (existingSavedUniversity != null)
            {
                // var isFavorited = true;
                return Json(new { isFavorited = true });
            }

            return Json(new { isFavorited = false });
        }


        [HttpPost]
        public async Task<IActionResult> SaveUniversity(string universityId)
        {
            var userId = _userManager.GetUserId(User);  // Get current logged-in user's id

            // First, check if the course is already saved by the user
            var existingSavedUniversity = await _context.UserSavedInfo
                .Where(u => u.UserId == userId && u.SchoolID == universityId)
                .FirstOrDefaultAsync();

            if (existingSavedUniversity != null)
            {
                // University is already saved, so remove it
                _context.UserSavedInfo.Remove(existingSavedUniversity);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "大学已移除收藏",isFavorited = false }); // university removed from saved
            }

            var universityToSave = await _universityContext.Universities
              
                .Where(c => c.SchoolID == universityId)
                .FirstOrDefaultAsync();

            if (universityToSave == null)
            {
                // Course not found
                return Json(new { success = false, message = "未找到大学" });
            }

            var userSavedInfo = new UserSavedInfo
            {
                UserId = userId,
                SchoolID = universityToSave.SchoolID,
                SchoolName = universityToSave.Name
            };

            _context.UserSavedInfo.Add(userSavedInfo);
            await _context.SaveChangesAsync();

            return Json(new { success = false, message = "大学收藏成功", isFavorited = true });
        }


        [HttpGet]
        public async Task<IActionResult> IsDocumentFavorited(string documentId)
        {
            var userId = _userManager.GetUserId(User);  // Get current logged-in user's id

            // First, check if the document is already saved by the user
            var existingSavedDocument = await _context.UserSavedInfo
                .Where(u => u.UserId == userId && u.DocumentID == documentId)
                .FirstOrDefaultAsync();

            if (existingSavedDocument != null)
            {
               // var isFavorited = true;
                return Json(new { isFavorited = true });
            }

            return Json(new { isFavorited = false });
        }


        [HttpPost]
        public async Task<IActionResult> SaveDocument(string documentId)
        {
            var userId = _userManager.GetUserId(User);  // Get current logged-in user's id

            // First, check if the document is already saved by the user
            var existingSavedDocument = await _context.UserSavedInfo
                .Where(u => u.UserId == userId && u.DocumentID == documentId)
                .FirstOrDefaultAsync();

            if (existingSavedDocument != null)
            {
                // Document is already saved
                //return Json(new { success = false, message = "文档已收藏" });

                // Document is already saved, so remove it
                _context.UserSavedInfo.Remove(existingSavedDocument);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "文档已移除收藏" , isFavorited = false}); // document removed from saved


            }

            var documentToSave = await _documentContext.Document
                .Include(d => d.University)
                .Include(d => d.Course)
                .Where(d => d.DocumentID == documentId)
                .FirstOrDefaultAsync();

            if (documentToSave == null)
            {
                // Document not found
                return Json(new { success = false, message = "未找到文档" });
            }

            var userSavedInfo = new UserSavedInfo
            {
                UserId = userId,
                DocumentID = documentToSave.DocumentID,
                DocumentName = documentToSave.Name,
                SchoolName=documentToSave.University.Name,
                CourseName=documentToSave.Course.CourseName
                
            };

            _context.UserSavedInfo.Add(userSavedInfo);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "文档收藏成功" , isFavorited = true });
        }

    }
}
