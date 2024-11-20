using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Lab5.Models;
using Lab5.Services;

namespace Lab5.Controllers;

public class AccountController(Auth0UserService auth0UserService) : Controller
{
    private readonly Auth0UserService _auth0UserService = auth0UserService;

    [HttpGet]
    public IActionResult Register()
    {
        return User.Identity != null && User.Identity.IsAuthenticated ? RedirectToAction("Profile", "Account") : View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            await _auth0UserService.CreateUserAsync(model);
            return RedirectToAction("Index", "Home");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"Error creating user: {ex.Message}");
            return View(model);
        }
    }

    [HttpGet]
    public IActionResult Login()
    {
        return User.Identity != null && User.Identity.IsAuthenticated ? RedirectToAction("Profile", "Account") : View();
    }


    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            ProfileViewModel userProfile = await _auth0UserService.GetUser(model);
            List<Claim> claims =
            [
                new Claim(ClaimTypes.NameIdentifier, userProfile.Email),
                    new Claim(ClaimTypes.Name, userProfile.FullName),
                    new Claim(ClaimTypes.Email, userProfile.Email),
                    new Claim("ProfileImage", userProfile.ProfileImage),
                    new Claim(ClaimTypes.MobilePhone, userProfile.Phone),
                    new Claim("Username", userProfile.UserName)
            ];

            ClaimsIdentity claimsIdentity = new(claims, "AuthScheme");
            ClaimsPrincipal claimsPrincipal = new(claimsIdentity);

            await HttpContext.SignInAsync("AuthScheme", claimsPrincipal);

            return RedirectToAction("Profile", "Account");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"Error authenticating user: {ex.Message}");
            return View(model);
        }
    }

    [Authorize]
    public IActionResult Profile()
    {
        string alternativeValue = "N/A";
        ClaimsPrincipal user = HttpContext.User;

        ProfileViewModel profileViewModel = new()
        {
            Email = user.FindFirst(ClaimTypes.Email)?.Value ?? alternativeValue,
            FullName = user.FindFirst(ClaimTypes.Name)?.Value ?? alternativeValue,
            Phone = user.FindFirst(ClaimTypes.MobilePhone)?.Value ?? alternativeValue,
            ProfileImage = user.FindFirst("ProfileImage")?.Value ?? alternativeValue,
            UserName = user.FindFirst("Username")?.Value ?? alternativeValue
        };

        return View(profileViewModel);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}