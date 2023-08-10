using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using StudentFileShare6.data;
using StudentFileShare6.Hubs;

namespace StudentFileShare6.Controllers
{
    public class LikeDislikeController : Controller
    {
        private readonly DocumentContext _context;

        public LikeDislikeController(DocumentContext context)
        {
            _context = context;
           

        }

     

        // Action method to handle liking a document
        [HttpPost]
        public IActionResult LikeDocument(string documentId)
        {
            // Retrieve the document from the database
            var document = _context.Document.FirstOrDefault(d => d.DocumentID == documentId);

            if (document == null)
            {
                return NotFound();
            }

            // Increase the like count for the document
            document.LikeNumber++;

            // Save the changes to the database
            _context.SaveChanges();

            // Return the updated like count
            return Json(document.LikeNumber);
        }

        // Action method to handle disliking a document
        [HttpPost]
        public IActionResult DislikeDocument(string documentId)
        {
            // Retrieve the document from the database
            var document = _context.Document.FirstOrDefault(d => d.DocumentID == documentId);

            if (document == null)
            {
                return NotFound();
            }

            // Increase the dislike count for the document
            document.DislikeNumber++;

            // Save the changes to the database
            _context.SaveChanges();

            // Return the updated dislike count
            return Json(document.DislikeNumber);
        }

        // Action method to handle canceling a like for a document
        [HttpPost]
        public IActionResult CancelLikeDocument(string documentId)
        {
            // Retrieve the document from the database
            var document = _context.Document.FirstOrDefault(d => d.DocumentID == documentId);

            if (document == null)
            {
                return NotFound();
            }

            // Decrease the like count for the document
            document.LikeNumber--;

            // Save the changes to the database
            _context.SaveChanges();

            // Return the updated like count
            return Json(document.LikeNumber);
        }

        // Action method to handle canceling a dislike for a document
        [HttpPost]
        public IActionResult CancelDislikeDocument(string documentId)
        {
            // Retrieve the document from the database
            var document = _context.Document.FirstOrDefault(d => d.DocumentID == documentId);

            if (document == null)
            {
                return NotFound();
            }

            // Decrease the dislike count for the document
            document.DislikeNumber--;

            // Save the changes to the database
            _context.SaveChanges();

            // Return the updated dislike count
            return Json(document.DislikeNumber);
        }

        // Action method to handle changing from like to dislike or vice versa
        [HttpPost]
        public IActionResult ChangeLikeDislikeDocument(string documentId, string action)
        {
            // Retrieve the document from the database
            var document = _context.Document.FirstOrDefault(d => d.DocumentID == documentId);

            if (document == null)
            {
                return NotFound();
            }

            // Update the like and dislike counts based on the action
            if (action == "like")
            {
                document.LikeNumber++;
                document.DislikeNumber--;
            }
            else if (action == "dislike")
            {
                document.LikeNumber--;
                document.DislikeNumber++;
            }

            // Save the changes to the database
            _context.SaveChanges();

            // Return the updated like and dislike counts
            return Json(new { likes = document.LikeNumber, dislikes = document.DislikeNumber });
        }

        [HttpPost]
        public ActionResult GetRating(string documentId)
        {
            var document = _context.Document.FirstOrDefault(d => d.DocumentID == documentId);

            if (document == null)
            {
                return NotFound();
            }
            float likeNumber = document.LikeNumber ?? 0f;
            float dislikeNumber = document.DislikeNumber ?? 0f;
            float totalVotes = likeNumber + dislikeNumber;
            float rating = (float)(totalVotes > 0 ? Math.Round((likeNumber / totalVotes) * 100f, 1) : 0f);
             document.Rating = rating;
           // document.Rating = (float?)0.7;
            _context.SaveChanges();
            return Content(rating.ToString());
        }


    }
}
