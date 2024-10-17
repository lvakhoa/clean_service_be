using CleanService.Src.Constant;
using CleanService.Src.Modules.Auth.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanService.Src.Modules.Auth;

[Authorize]
[Route("/auth/[action]")]
public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    public IActionResult Index()
    {
        return Content(User.Identity?.Name ?? "");
    }

    public IActionResult Login()
    {
        // return Content(User.Identity?.Name ?? "");
        return Challenge(new AuthenticationProperties()
            {
                RedirectUri = "http://localhost:5011/auth/index"
            },
            authenticationSchemes: new[] { AuthProvider.Provider });
    }
}