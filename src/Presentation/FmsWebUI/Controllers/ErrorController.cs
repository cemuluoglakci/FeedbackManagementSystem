using Microsoft.AspNetCore.Mvc;

namespace FmsWebUI.Controllers
{
    public class ErrorController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }


    }
}
