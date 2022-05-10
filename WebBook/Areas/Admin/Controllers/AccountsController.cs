using Domin.Entity;
using Infarstuructre.Data;
using Infarstuructre.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountsController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly FreeBookDbContext _context;

        public AccountsController(RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager ,FreeBookDbContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public IActionResult Roles()
        {
            return View(new RolesViewModel
            {
                NewRole = new NewRole(),
                Roles = _roleManager.Roles.OrderBy(x => x.Name).ToList()
            });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Roles(RolesViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = new IdentityRole
                {
                    Id = model.NewRole.RoleId,
                    Name = model.NewRole.RoleName
                };
                // Create
                if(role.Id == null)
                {
                    role.Id = Guid.NewGuid().ToString();
                    var result = await _roleManager.CreateAsync(role);
                    if (result.Succeeded)
                    {
                        // Succeeded
                        HttpContext.Session.SetString("msgType","success");
                        HttpContext.Session.SetString("title", Resource.ResourceWeb.lbSave);
                        HttpContext.Session.SetString("msg", Resource.ResourceWeb.lbSaveMsgRole);
                    }
                    else
                    {
                        // Not Successeded
                        HttpContext.Session.SetString("msgType", "error");
                        HttpContext.Session.SetString("title", Resource.ResourceWeb.lbNotSaved);
                        HttpContext.Session.SetString("msg", Resource.ResourceWeb.lbNotSavedMsgRole);
                    }

                }//Update
                else
                {
                    var RoleUpdate =await _roleManager.FindByIdAsync(role.Id);
                    RoleUpdate.Id = model.NewRole.RoleId;
                    RoleUpdate.Name = model.NewRole.RoleName;
                    var Result = await _roleManager.UpdateAsync(RoleUpdate);
                    if (Result.Succeeded)
                    {
                        // Succeeded
                        HttpContext.Session.SetString("msgType", "success");
                        HttpContext.Session.SetString("title", Resource.ResourceWeb.lbUpdate);
                        HttpContext.Session.SetString("msg", Resource.ResourceWeb.lbUpdateMsgRole);
                    }
                    else
                    {
                        // Not Successeded
                        HttpContext.Session.SetString("msgType", "error");
                        HttpContext.Session.SetString("title", Resource.ResourceWeb.lbNotUpdate);
                        HttpContext.Session.SetString("msg", Resource.ResourceWeb.lbNotUpdateMsgRole);
                    }
                }
            }
            return RedirectToAction("Roles");
        }
        public async Task<IActionResult> DeleteRole(string Id)
        {
            var role = _roleManager.Roles.FirstOrDefault(x => x.Id == Id);
            if((await _roleManager.DeleteAsync(role)).Succeeded)
            {
                return RedirectToAction(nameof(Roles));
            }
            return RedirectToAction("Roles");
        }

        public IActionResult Registers()
        {
            var model = new RegisterViewModel
            {
                NewRegister = new NewRegister(),
                Roles = _roleManager.Roles.OrderBy(x => x.Name).ToList(),
                Users = _context.VwUsers.OrderBy(x => x.Role).ToList() //_userManager.Users.OrderBy(x=>x.Name).ToList()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registers(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var file = HttpContext.Request.Form.Files;
                if (file.Count() > 0)
                {
                    string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                    var fileStream = new FileStream(Path.Combine(@"wwwroot/",Helper.PathSaveImageuser,ImageName),FileMode.Create);
                    file[0].CopyTo(fileStream);
                    model.NewRegister.ImageUser = ImageName;
                }
                else
                {
                    model.NewRegister.ImageUser = model.NewRegister.ImageUser;
                }
                var user = new ApplicationUser
                {
                    Id = model.NewRegister.Id,
                    Name = model.NewRegister.Name,  
                    UserName = model.NewRegister.Email,
                    Email = model.NewRegister.Email,
                    ActiveUser = model.NewRegister.ActiveUser,
                    ImageUser = model.NewRegister.ImageUser
                };
                if(user.Id == null)
                {
                    //Craete
                    user.Id = Guid.NewGuid().ToString();
                  var result = await _userManager.CreateAsync(user,model.NewRegister.Password);
                    if (result.Succeeded)
                    {
                        //Succsseded
                        var Role =await _userManager.AddToRoleAsync(user,model.NewRegister.RoleName);
                        if (Role.Succeeded)
                        {
                            HttpContext.Session.SetString("msgType", "success");
                            HttpContext.Session.SetString("title", Resource.ResourceWeb.lbSave);
                            HttpContext.Session.SetString("msg", Resource.ResourceWeb.lbNotSavedMsgUserRole);
                        }
                        else
                        {
                            HttpContext.Session.SetString("msgType", "error");
                            HttpContext.Session.SetString("title", Resource.ResourceWeb.lbNotSaved);
                            HttpContext.Session.SetString("msg", Resource.ResourceWeb.lbNotSavedMsgUser);
                        }
                    }
                    else
                    {
                        //Not Successeded
                        HttpContext.Session.SetString("msgType", "error");
                        HttpContext.Session.SetString("title", Resource.ResourceWeb.lbNotSaved);
                        HttpContext.Session.SetString("msg", Resource.ResourceWeb.lbNotUpdateMsgUser);
                    }

                }
                else
                {
                    //Update
                    var userUpdate =await _userManager.FindByIdAsync(user.Id);
                    userUpdate.Id = model.NewRegister.Id;
                    userUpdate.Name = model.NewRegister.Name;
                    userUpdate.UserName = model.NewRegister.Email;
                    userUpdate.Email = model.NewRegister.Email;
                    userUpdate.ActiveUser = model.NewRegister.ActiveUser;
                    userUpdate.ImageUser = model.NewRegister.ImageUser;

                    var result =await _userManager.UpdateAsync(userUpdate);
                    if (result.Succeeded)
                    {
                        var oldRole = await _userManager.GetRolesAsync(userUpdate);
                        await _userManager.RemoveFromRolesAsync(userUpdate,oldRole);
                        var AddRole =await _userManager.AddToRoleAsync(userUpdate,model.NewRegister.RoleName);
                        if (AddRole.Succeeded)
                        {
                            HttpContext.Session.SetString("msgType", "success");
                            HttpContext.Session.SetString("title", Resource.ResourceWeb.lbUpdate);
                            HttpContext.Session.SetString("msg", Resource.ResourceWeb.lbNotUpdateMsgUserRole);
                        }
                        else
                        {
                            HttpContext.Session.SetString("msgType", "error");
                            HttpContext.Session.SetString("title", Resource.ResourceWeb.lbNotUpdate);
                            HttpContext.Session.SetString("msg", Resource.ResourceWeb.lbNotUpdateMsgUserRole);
                        }
                    }
                    else
                    {
                        // Not Successeded
                        HttpContext.Session.SetString("msgType", "error");
                        HttpContext.Session.SetString("title", Resource.ResourceWeb.lbNotUpdate);
                        HttpContext.Session.SetString("msg", Resource.ResourceWeb.lbNotUpdateMsgUser);
                    }
                }
                return RedirectToAction("Registers", "Accounts");
            }
            return RedirectToAction("Registers","Accounts");
        }

        public async Task<IActionResult> DeleteUser(string userId)
        {
            var User = _userManager.Users.FirstOrDefault(x=>x.Id == userId);

            if(User.ImageUser != null && User.ImageUser != Guid.Empty.ToString())
            {
                var PathImage = Path.Combine(@"wwwroot/", Helper.PathImageuser, User.ImageUser);
                if (System.IO.File.Exists(PathImage))
                    System.IO.File.Delete(PathImage);
            }
           if(( await _userManager.DeleteAsync(User)).Succeeded)
            {
                return RedirectToAction("Registers", "Accounts");
            }
            return RedirectToAction("Registers", "Accounts");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(RegisterViewModel model)
        {
            var user =await _userManager.FindByIdAsync(model.ChangePassword.Id);
            if(user != null)
            {
                await _userManager.RemovePasswordAsync(user);
                var AddNewPassword = await _userManager.AddPasswordAsync(user,model.ChangePassword.NewPassword);
                if (AddNewPassword.Succeeded)
                {
                    HttpContext.Session.SetString("msgType", "success");
                    HttpContext.Session.SetString("title", Resource.ResourceWeb.lbSave);
                    HttpContext.Session.SetString("msg", Resource.ResourceWeb.lbMsgSavedChangePassword);
                }
                else
                {
                    HttpContext.Session.SetString("msgType", "error");
                    HttpContext.Session.SetString("title", Resource.ResourceWeb.lbNotSaved);
                    HttpContext.Session.SetString("msg", Resource.ResourceWeb.lbMsgNotSavedChangePassword);

                }
                return RedirectToAction(nameof(Registers));
            }

            return RedirectToAction(nameof(Registers));

        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login (LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var Result = await _signInManager.PasswordSignInAsync(model.Eamil,
                    model.Password,model.RememberMy,false);
                if (Result.Succeeded)
                    return RedirectToAction("Index", "Home");
                else
                    ViewBag.ErrorLogin = false;

            }
            return View(model);
        }

    }
}
