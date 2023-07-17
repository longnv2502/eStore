using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApp.Models;
using BussinessObject.Models;
using System.Net.Http;
using DataAccess.Models;
using System.Text.Json;
using System.Net.Http.Headers;
using System.Net;
using System.IdentityModel.Tokens.Jwt;

namespace WebApp.Controllers
{
    [Route("auth")]
    public class AuthController : Controller
    {
        private readonly HttpClient _httpClient;

        private readonly ILogger<HomeController> _logger;

        public AuthController(ILogger<HomeController> logger)
        {
            _logger = logger;
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            _httpClient = new HttpClient(clientHandler);
        }

        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                string urlLogin = @"https://localhost:5000/api/auth/login";
                HttpResponseMessage responseMessage = await _httpClient.PostAsJsonAsync(urlLogin, userDTO);
                if (responseMessage.StatusCode == HttpStatusCode.Accepted)
                {
                    var token = await responseMessage.Content.ReadFromJsonAsync<TokenRequest>();
                    var handler = new JwtSecurityTokenHandler();
                    var jwtSecurityToken = handler.ReadJwtToken(token?.Token);
                    ClaimsIdentity identity = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.Name).Value),
                        new Claim(ClaimTypes.Role, jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.Role).Value),
                    }, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    return Ok(token);
                }
                return NotFound();
            }

            catch (Exception)
            {
                return Problem($"Something Went Wrong in the {nameof(Login)}", statusCode: 500);
            }
        }

        [Route("register")]
        public IActionResult Register()
        {
            return View();
        }

        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
