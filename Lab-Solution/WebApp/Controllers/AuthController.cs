using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace WebApp.Controllers
{
    [Route("auth")]
    public class AuthController : Controller
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        [Route("login")]
        public IActionResult Login()
        {
            //var account = await _context.Accounts.FirstOrDefaultAsync(a => a.UserName.Equals(username) && a.Password.Equals(password));
            //if (account == null)
            //{
            //    UserName = username;
            //    Password = password;
            //    return View();
            //}
            //else
            //{
            //    var role = account.Type == 0 ? "Member" : "Admin";
            //    ClaimsIdentity identity = new ClaimsIdentity(new[]
            //    {
            //        new Claim(ClaimTypes.Name, username),
            //        new Claim(ClaimTypes.Role, role),
            //        new Claim("AccountId", account.AccountId.ToString())
            //    }, CookieAuthenticationDefaults.AuthenticationScheme);
            //    var principal = new ClaimsPrincipal(identity);

            //    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            //    return RedirectToPage("/Index");
            //}
            return View();
        }

        [Route("register")]
        public IActionResult Register()
        {
            return View();
        }
    }
}
