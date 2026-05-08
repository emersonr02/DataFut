using Microsoft.AspNetCore.Mvc;

namespace DataFut.Controllers
{
    public class ClubeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
