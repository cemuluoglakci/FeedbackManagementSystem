using ApplicationFMS.Handlers.Account.Commands.RegisterUser;
using ApplicationFMS.Handlers.LookUp.LookupEducation;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ApplicationFMS.Handlers.Account.Commands.VerifyEmail;
using ApplicationFMS.Handlers.Account.Queries.UserLogin;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Org.BouncyCastle.Asn1.Ocsp;

namespace FmsWebUI.Controllers
{
    public class UserController : BaseController
    {


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginQuery request)
        {
            var response = await Mediate(request);
            if (response.Meta.SuccessStatus)
            {
                HttpContext.Response.Cookies.Append("Authorization", response.data.ToString(), new CookieOptions { HttpOnly = true });
                return RedirectToAction("List",  "Feedback");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {

            return View();
        }

        [HttpGet]
        public IActionResult LogOut()
        {
            Response.Cookies.Delete("Authorization");
            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserCommand request)
        {
            var response = await Mediate(request);
            if (response.Meta.SuccessStatus)
                return RedirectToAction("Verify");
            return View();
        }

        [HttpGet]
        public IActionResult Verify()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Verify(VerifyEmailCommand request)
        {
            var response = await Mediate(request);
            if (response.Meta.SuccessStatus)
            {
                return RedirectToAction("Verified");
            }
            return View();
        }

        [HttpGet]
        public IActionResult VerifyFromMail(string email, string verificationCode)
        {
            var response = Mediate(new VerifyEmailCommand() { Email = email, VerificationCode = verificationCode }).Result;
            if (response.Meta.SuccessStatus)
            {
                return RedirectToAction("Verified");
            }
            else
            {
                return RedirectToAction("Verify");
            }
        }

        [HttpGet]
        public IActionResult Verified()
        {

            return View();
        }

    }
}
