using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Route("admin")]
    public class AdminController : Controller
    {
        [Route("users")]
        public IActionResult UserIndex()
        {
            return View();
        }

        [Route("orders")]
        public IActionResult OrderIndex()
        {
            return View();
        }

        [Route("orders/detail")]
        public IActionResult OrderDetail()
        {
            return View();
        }

        [Route("orders/edit")]
        public IActionResult OrderEdit()
        {
            return View();
        }

        [Route("products")]
        public IActionResult ProductIndex()
        {
            return View();
        }

        [Route("products/detail")]
        public IActionResult ProductDetail()
        {
            return View();
        }

        [Route("products/create")]
        public IActionResult ProductCreate()
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
