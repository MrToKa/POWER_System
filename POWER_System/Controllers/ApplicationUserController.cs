﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using POWER_System.Models;
using POWER_System.Models.ApplicationUserModels;

namespace POWER_System.Controllers;

[Authorize]
public class ApplicationUserController : Controller
{
    private readonly UserManager<ApplicationUser> userManager;

    private readonly SignInManager<ApplicationUser> signInManager;

    public ApplicationUserController(
        UserManager<ApplicationUser> _userManager,
        SignInManager<ApplicationUser> _signInManager)
    {
        userManager = _userManager;
        signInManager = _signInManager;
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Register()
    {
        var model = new RegisterViewModel();

        return View(model);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = new ApplicationUser()
        {
            Email = model.Email,
            UserName = model.Username,
            FirstName = model.FirstName,
            LastName = model.LastName,
            OfficeLocation = model.OfficeLocation,
            Department = model.Department,
            Position = model.Position,
            CreatedOn = DateTime.Now,
        };

        var result = await userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            return RedirectToAction("Login", "ApplicationUser");
        }

        foreach (var item in result.Errors)
        {
            ModelState.AddModelError("", item.Description);
        }

        return View(model);
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login()
    {
        var model = new LoginViewModel();

        return View(model);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await userManager.FindByNameAsync(model.UserName);

        if (user != null)
        {
            var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
        }

        ModelState.AddModelError("", "Invalid login");

        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();

        return RedirectToAction("Index", "Home");
    }
}