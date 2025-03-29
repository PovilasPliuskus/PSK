using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TaskManager.API.Controllers
{
    public class LoginController : Controller
    {
        public LoginController()
        {
        }

        [HttpGet("sign-in")]
        public IActionResult Signin()
        {
            return RedirectToAction(nameof(GetHomePage));
        }


        [HttpPost("sign-out")]
        public async Task<IActionResult> Sign_out() // different name to avoid conflict with base.SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
            
            return RedirectToAction(nameof(Signin));
        }
        
        [Authorize]
        [HttpGet("api/home")]
        public async Task<IActionResult> GetHomePage()
        {
            var authentication = await HttpContext.AuthenticateAsync();
            var claimsPrincipal = authentication.Principal;
            
            var claimsDictionary = claimsPrincipal?.Claims
                .ToDictionary(c => c.Type, c => c.Value);

            return Json(claimsDictionary, new JsonSerializerOptions { WriteIndented = true });
        }
    }
}

