using Microsoft.AspNetCore.Mvc;
using WorkTrack.Services;
using WorkTrack.ViewModels.Auth;
using WorkTrack.DTO;

namespace WorkTrack.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        /*
         ------------------------
         LOGIN
         ------------------------
        */

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var dto = new LoginDto
            {
                Email = vm.Email,
                Password = vm.Password
            };

            var result = await _authService.LoginAsync(dto);

            if (!result.Success)
            {
                ModelState.AddModelError("", result.Error!);
                return View(vm);
            }

            return RedirectToAction("Index", "Home");
        }

        /*
         ------------------------
         REGISTER
         ------------------------
        */

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var dto = new SignUpDto
            {
                Email = vm.Email,
                Password = vm.Password,
                FullName = vm.FullName
            };

            var result = await _authService.RegisterAsync(dto);

            if (!result.Success)
            {
                ModelState.AddModelError("", result.Error!);
                return View(vm);
            }

            return RedirectToAction("Login");
        }

        /*
         ------------------------
         LOGOUT
         ------------------------
        */

        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return RedirectToAction("Login");
        }
    }
}