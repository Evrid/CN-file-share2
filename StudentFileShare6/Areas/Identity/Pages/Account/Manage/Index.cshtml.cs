// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;


namespace StudentFileShare6.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private IMemoryCache _cache;

        public IndexModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IMemoryCache memoryCache
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _cache = memoryCache;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>


        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Phone]
            [Required(ErrorMessage = "电话为必填项。")]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [Required(ErrorMessage = "用户名为必填项。")]
            [Display(Name = "用户名")]
            [StringLength(10, MinimumLength = 4, ErrorMessage = "用户名长度必须在4到10个字符之间。")]
            public string UserName { get; set; }


            [Required(ErrorMessage = "验证码为必填项。")]
            [Display(Name = "验证码")]
            public string VerificationCode { get; set; }

        }

        private async Task LoadAsync(IdentityUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);



            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                UserName = userName
            };
        }


        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);




            if (user == null)
            {
                return NotFound($"无法加载用户 ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);


            if (user == null)
            {
                return NotFound($"无法加载用户 ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                //foreach (var modelStateValue in ModelState.Values)
                //{
                //    foreach (var error in modelStateValue.Errors)
                //    {
                //        var errorMessage = error.ErrorMessage;
                //        // Log or print the errorMessage to see what errors are present.
                //    }
                //}
                await LoadAsync(user);
                return Page();
            }


            if (_cache.TryGetValue(Input.PhoneNumber, out string storedCode))
            {
                if (storedCode != Input.VerificationCode)
                {
                    ModelState.AddModelError(string.Empty, "验证码错误。");
                    return Page();
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "验证码已过期或错误。");
                return Page();
            }


            if (Input.UserName != userName)
            {
                // Set the new username for the user
                var setUserNameResult = await _userManager.SetUserNameAsync(user, Input.UserName);
                if (!setUserNameResult.Succeeded)
                {
                    StatusMessage = "尝试设置用户名时出现意外错误";

                }


            }

            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "尝试设置电话号码时出现意外错误";
                    return RedirectToPage();
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "你的个人资料已经更新";
            return RedirectToPage();
        }
    }
}