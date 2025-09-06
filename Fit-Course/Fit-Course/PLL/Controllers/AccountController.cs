using BLL.DTOS.AccountDTOS;
using BLL.Services;
using BLL.Services.account;
using BLL.Services.UserServices;
using DAL.Entities.user;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace PLL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IEmailSender emailSender;
        private readonly IAccountService accountService;

        public AccountController(UserManager<User> _userManager, SignInManager<User> _signInManager, IEmailSender _emailSender, IAccountService _accountService)
        {
            signInManager = _signInManager;
            userManager = _userManager;
            emailSender = _emailSender;
            accountService = _accountService;


        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register() { return View(); }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDTO RegDTO)
        {
            if (ModelState.IsValid == true)
            {
                //mapping
                User appUser = new User
                {
                    FullName = RegDTO.FName + " " + RegDTO.LName,
                    Email = RegDTO.Email,
                    CreatedOn = DateTime.Now,
                    UserName = RegDTO.Email,
                    IsBLocked = false
                };

                //save database 
                IdentityResult result = await userManager.CreateAsync(appUser, RegDTO.Password);
                if (result.Succeeded)
                {
                    //set cookie by using signInManager
                    await signInManager.SignInAsync(appUser, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }

            }
            return View(RegDTO);
        }
        public async Task<IActionResult> isUnique(string email)
        {
            User exists = await userManager.FindByEmailAsync(email);
            if (exists != null)
            {
                return Json($"Already used before, Try to login");
            }

            return Json(true);
        }

        [HttpGet]
        public IActionResult Login() { return View(); }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDTO logDTO)
        {
            if (ModelState.IsValid == true)
            {
                //check found
                User user = await userManager.FindByEmailAsync(logDTO.Email);
                if (user != null)
                {
                    //check pass & email [we send user to check if the both together]
                    bool found = await userManager.CheckPasswordAsync(user, logDTO.Password);
                    if (found == true)
                    {
                        //cookie
                        await signInManager.SignInAsync(user, isPersistent: logDTO.RememberMe);
                        //Redirect based on the role
                        return RedirectToAction("Index", "Home");

                    }
                }
                ModelState.AddModelError("", "Email or Pasword Wrong");
            }
            return View(logDTO);
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return View("Login");
        }





        // Forgot     verify email      resetpassword
        // change password
        [HttpGet]
        public IActionResult ForgotPassword() => View();

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDTO model)
        {
            if (!ModelState.IsValid) return View(model);

            var success = await accountService.SendVerificationCodeAsync(model.Email, "PasswordReset");
            if (!success)
            {
                ModelState.AddModelError("", "Email not found.");
                return View(model);
            }

            return RedirectToAction("VerifyEmailCode", new { email = model.Email });
        }


        [HttpGet]
        public IActionResult VerifyEmailCode(string email)
        {
            var model = new VerifyCodeDTO { Email = email };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> VerifyEmailCode(VerifyCodeDTO model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await accountService.ConfirmEmailCodeAsync(model);
            if (!result.Success)
            {
                ModelState.AddModelError("", result.ErrorMessage ?? "Invalid or expired code.");
                return View(model);
            }

            return RedirectToAction("ResetPassword", new { email = model.Email });
        }


        [HttpGet]
        public IActionResult ResetPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
                return RedirectToAction("Login");

            var model = new ResetPasswordDTO { Email = email };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid user.");
                return View(model);
            }

            var result = await userManager.RemovePasswordAsync(user);
            if (result.Succeeded)
            {
                result = await userManager.AddPasswordAsync(user, model.Password);
                if (result.Succeeded)
                {
                    user.ResetCode = null;
                    user.ResetCodeExpiry = null;
                    await userManager.UpdateAsync(user);

                    TempData["PasswordResetSuccess"] = true;
                    return RedirectToAction("ResetPassword", new { email = model.Email });
                }
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(model);
        }

        [HttpGet]
        public IActionResult ResetPasswordConfirmation() => View();

        [HttpPost]
        public async Task<IActionResult> ResendCode(string email)
        {
            if (string.IsNullOrEmpty(email))
                return RedirectToAction("ForgotPassword");

            var success = await accountService.SendVerificationCodeAsync(email, "Password Reset");
            if (!success)
            {
                TempData["Error"] = "Unable to resend code. Please try again.";
            }
            else
            {
                TempData["Message"] = "New code has been sent to your email.";
            }

            return RedirectToAction("VerifyEmailCode", new { email });
        }





    }
}