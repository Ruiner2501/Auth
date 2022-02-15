using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Auth.Models;
using Auth.ViewModels;
using System;
using Microsoft.AspNetCore.Authorization;

namespace Auth.Controllers
{   [Authorize]
    public class UsersController : Controller

    {   
        UserManager<User> _userManager;

        public UsersController(UserManager<User> userManager)
        {
            _userManager = userManager;
            
           
        }

        public IActionResult UsersAdm() => View(_userManager.Users.ToList());

        public IActionResult Create() => View();




        [HttpPost]
        public async Task<ActionResult> Delete(string[] SelectedUsers)
        {
            if (SelectedUsers != null)
            {
                foreach (var i in SelectedUsers)
                {
                    var user = await _userManager.FindByIdAsync(i);
                    if (user != null)
                    {
                        await _userManager.DeleteAsync(user);
                        if (User.Identity.Name == user.UserName)
                            return RedirectToAction("Login", "Account");
                    }
                    else
                        ModelState.AddModelError("", "Nein!");
                }
            }
            return RedirectToAction("UsersAdm");
        }



        [HttpPost]
        public async Task<ActionResult> LockOut(string[] SelectedUsers)
        {
            if (SelectedUsers != null)
            {
                foreach (var i in SelectedUsers)
                {
                    var user = await _userManager.FindByIdAsync(i);

                    if (User.Identity.Name == user.UserName)
                    {
                        user.IsLocked = true;
                        user.LockoutEnabled = true;
                        user.LockoutEnd = DateTime.MaxValue;
                        await _userManager.UpdateAsync(user);
                        return RedirectToAction("Login", "Account");
                    }
                    else
                    {
                        if (user != null)
                        {
                            user.IsLocked = true;
                            user.LockoutEnabled = true;
                            user.LockoutEnd = DateTime.MaxValue;
                            await _userManager.UpdateAsync(user);
                        }
                        else
                            ModelState.AddModelError("", "Unable to find user");
                    }
                }
            }
            return RedirectToAction("UsersAdm");
        }


        [HttpPost]
        public async Task<ActionResult> Unlock(string[] SelectedUsers)
        {
            if (SelectedUsers != null)
            {
                foreach (var i in SelectedUsers)
                {
                    var user = await _userManager.FindByIdAsync(i);
                    if (user != null)
                    {
                        user.IsLocked = false;
                        user.LockoutEnabled = false;
                        user.LockoutEnd = DateTime.Now;
                        await _userManager.UpdateAsync(user);
                    }
                    else
                        ModelState.AddModelError("", "Unable to find user");
                }
            }
            return RedirectToAction("UsersAdm");
        }







    }
}
