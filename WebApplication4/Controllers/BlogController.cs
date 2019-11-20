using Microsoft.AspNetCore.Mvc;

namespace Blog.Core.Controllers
{
    public class BlogController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return
            View();
        }
    }
}