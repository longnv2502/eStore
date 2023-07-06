using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Route("admin")]
    public class AdminController : Controller
    {
        [Route("users")]
        public IActionResult UserIndex ()
        {
            return View(); 
        }

        [Route("orders")]
        public IActionResult OrderIndex()
        {
            return View();
        }

        [Route("products")]
        public IActionResult ProductIndex()
        {
            return View();
        }

        [Route("products/edit")]
        public IActionResult ProductEdit()
        {
            return View();
        }
    }
}
