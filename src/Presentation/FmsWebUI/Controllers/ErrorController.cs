using Microsoft.AspNetCore.Mvc;

namespace FmsWebUI.Controllers
{
    public class ErrorController : BaseController
    {
        public IActionResult Index(string errorMessage)
        {
            return View((object)errorMessage);
        }


    }
}
