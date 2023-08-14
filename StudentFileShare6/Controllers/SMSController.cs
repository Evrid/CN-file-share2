using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StudentFileShare6.Controllers
{
    public class SMSController : Controller
    {

        private IMemoryCache _cache;

        public SMSController(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        public IActionResult Index()
        {
            return View();
        }


        public void StoreCodeInCache(string phoneNumber, string code)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
            };
            _cache.Set(phoneNumber, code, cacheEntryOptions);
        }


        [HttpPost]
        public IActionResult GenerateCode(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                // Handle error - phone number is required
                return Json(new { success = false, message = "Phone number is required." });
            }

            Random random = new Random();
            string code = random.Next(100000, 999999).ToString();

            // Store code in cache with expiration
            StoreCodeInCache(phoneNumber, code);

            // Send the code to user's phone using your SMS gateway service

           // bool isSent = SendCodeViaSms(phoneNumber, code);

            bool isSent = true;  //for now not send yet

            System.Diagnostics.Debug.WriteLine(phoneNumber);
            System.Diagnostics.Debug.WriteLine(code);

            if (isSent)
            {
                return Json(new { success = true, message = "Code sent successfully!" });

              
            }
            else
            {
                // Handle error - maybe the SMS service failed or something else went wrong
                return Json(new { success = false, message = "Failed to send code. Please try again." });
            }
        }

        //private bool SendCodeViaSms(string phoneNumber, string code)
        //{
        //    // Here, you'd integrate with an SMS gateway to send the code.
        //    // For example, using a service like Twilio.
        //    // Return true if the message was sent successfully, otherwise return false.
        //}

    }
}
