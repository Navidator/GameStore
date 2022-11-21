using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
