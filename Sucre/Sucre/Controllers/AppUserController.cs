using Humanizer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sucre_Core;
using Sucre_DataAccess.Repository;
using Sucre_Models;
using Sucre_Services.Interfaces;
using System.Security.Claims;

namespace Sucre.Controllers
{
    [Authorize]
    public class AppUserController : Controller
    {
        private readonly IUserService _userService;

        public AppUserController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> AppUserRegister()
        {
            //var model = new AppUserRegisterM();
            //if (modelIn != null)
            //{
            //    model.Email = modelIn.Email;
            //    model.Password = modelIn.Password;
            //    model.PasswordConfirmation = modelIn.PasswordConfirmation;
            //}
            
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        [ActionName("AppUserRegister")]
        public async Task<IActionResult> AppUserRegisterPost(AppUserRegisterM model)
        {
            //var jjj = DelListUser.ListUser;

        //    if (model.Password.IsNullOrEmpty()) 
        //        { ModelState.AddModelError("Password", "Password null"); };
            if (ModelState.IsValid) 
            {
                //if (DelListUser.ListUser.Count > 0 &&
                //    DelListUser.ListUser.FirstOrDefault(i => i.Email == model.Email) != null)
                if (await _userService.IsUserExist(model.Email))
                {
                    var gg = _userService.IsUserExist(model.Email);
                    ModelState.AddModelError("Email", "Email is used");
                }
                else 
                {
                    

                    var dto = new AppUserRegTdo()
                    {
                        Email = model.Email,
                        Password = model.Password
                    };
                    _userService.RegisterUser(dto);

                    var ht = HttpContext.User.Identity.IsAuthenticated;

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal( await _userService.Authenticate(dto.Email))); 

                    return RedirectToAction("Index", "Home");
                    //ModelState.AddModelError("Email", "tratataaaa");
                };
                
            }
            //else 
            //{
                AppUserRegisterM modelOut = new AppUserRegisterM
                {
                    Email = model.Email,
                    Password = model.Password,
                    PasswordConfirmation = model.PasswordConfirmation
                };
                return View(modelOut);
            //}
            //return View(model);
            
        }
        
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> AppUserLogin()
        {
           
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        [ActionName("AppUserLogin")]
        public async Task<IActionResult> AppUserLoginPost(AppUserLoginM model)
        {
            if (ModelState.IsValid)
            {
                //if (DelListUser.ListUser.Count > 0 &&
                //    DelListUser.ListUser.FirstOrDefault(i => i.Email == model.Email) != null)
                if (await _userService.IsUserExist(model.Email))
                {
                    var gg = _userService.getmd5hash(model.Password);
                    bool gd = await _userService.IsPasswordCorrect(model.Email, model.Password);
                    //var gg = _userService.IsUserExist(model.Email);

                    if (gd)
                    {
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                             new ClaimsPrincipal(await _userService.Authenticate(model.Email)));
                        return RedirectToAction("Index", "Home");

                    }
                    else
                    {
                        ModelState.AddModelError("Password", "Password is not correct");
                    }
                }
                else
                {

                    ModelState.AddModelError("Email", "Email is not found");
                    //ModelState.AddModelError("Email", "tratataaaa");
                };

            }
            //else 
            //{
            AppUserRegisterM modelOut = new AppUserRegisterM
            {
                Email = model.Email,
                Password = model.Password,
                //PasswordConfirmation = model.PasswordConfirmation
            };
            return View(modelOut);
            //}
            //return View(model);

        }
        [HttpGet]
        public async Task<IActionResult> AppUserLogout()
        {
            HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

    }

    
}
