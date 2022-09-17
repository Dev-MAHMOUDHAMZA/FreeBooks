using Domin.Entity;
using Infarstuructre.IRepository;
using Infarstuructre.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;

namespace WebBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly IServicesRepository<Category> _servicesCategory;
        private readonly IServicesRepositoryLog<LogCategory> _servicesCategoryLog;
        private readonly UserManager<ApplicationUser> _userManager;

        public CategoriesController(IServicesRepository<Category> servicesCategory ,
            IServicesRepositoryLog<LogCategory> servicesCategoryLog,
            UserManager<ApplicationUser> userManager)
        {
            _servicesCategory = servicesCategory;
            _servicesCategoryLog = servicesCategoryLog;
            _userManager = userManager;
        }
        public IActionResult Categories()
        {
            return View(new CategoryViewModel
            {
               Categories = _servicesCategory.GetAll(),
               LogCategories = _servicesCategoryLog.GetAll(),
               NewCategory = new Category()
            });
        }

        public IActionResult Delete(Guid Id)
        {
            var userId = _userManager.GetUserId(User);
            if (_servicesCategory.Delete(Id) && _servicesCategoryLog.Delete(Id, Guid.Parse(userId)))
                return RedirectToAction(nameof(Categories));
            return RedirectToAction(nameof(Categories));

        }
        public IActionResult DeleteLog(Guid Id)
        {
            if (_servicesCategoryLog.DeleteLog(Id))
                return RedirectToAction(nameof(Categories));
            return RedirectToAction(nameof(Categories));
        }
       
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Save(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);

                if(model.NewCategory.Id == Guid.Parse(Guid.Empty.ToString()))
                { //Save
                    if (_servicesCategory.FindBy(model.NewCategory.Name) != null)
                        SessionMsg(Helper.Error,Resource.ResourceWeb.lbNotSaved,Resource.ResourceWeb.lbMsgDuplicateNameCategory);
                    else
                    {
                        if(_servicesCategory.Save(model.NewCategory) 
                            && _servicesCategoryLog.Save(model.NewCategory.Id,Guid.Parse(userId)))
                            SessionMsg(Helper.Success, Resource.ResourceWeb.lbSave, Resource.ResourceWeb.lbMsgSaveCategory);
                        else
                            SessionMsg(Helper.Error, Resource.ResourceWeb.lbNotSaved, Resource.ResourceWeb.lbMsgNotSavedCategory);
                    }
                }
                else 
                { //Update
                    if (_servicesCategory.Save(model.NewCategory)
                            && _servicesCategoryLog.Update(model.NewCategory.Id, Guid.Parse(userId)))
                        SessionMsg(Helper.Success, Resource.ResourceWeb.lbUpdate, Resource.ResourceWeb.lbMsgUpdateCategory);
                    else
                        SessionMsg(Helper.Error, Resource.ResourceWeb.lbNotSaved, Resource.ResourceWeb.lbMsgNotUpdatedCategory);
                }
            }
            return RedirectToAction(nameof(Categories));

        }

        private void SessionMsg(string MsgType, string Title, string Msg)
        {
            HttpContext.Session.SetString(Helper.MsgType, MsgType);
            HttpContext.Session.SetString(Helper.Title, Title);
            HttpContext.Session.SetString(Helper.Msg, Msg);
        }
    }
}
