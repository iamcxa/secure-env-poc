using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
        public IActionResult Error() => View();
    }
}
