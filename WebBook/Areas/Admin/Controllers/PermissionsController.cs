using Domin.Constants;
using Domin.Entity;
using Infarstuructre.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PermissionsController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public PermissionsController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            var claims = _roleManager.GetClaimsAsync(role).Result.Select(x=>x.Value).ToList();
            var allPermissions = Permissions.PermissionsList()
                    .Select(x => new RoleClaimsViewModel { Value = x }).ToList();
            foreach (var permission in allPermissions)
                if (claims.Any(x => x == permission.Value))
                    permission.Selected = true;

            return View(new PermissionViewModel
            {
                RoleId = roleId,    
                RoleName = role.Name,   
                RoleClaims = allPermissions
            });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update (PermissionViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.RoleId);
            var claims = await _roleManager.GetClaimsAsync(role);
            foreach (var claim in claims)
                await _roleManager.RemoveClaimAsync(role, claim);

            var SelectedClaims = model.RoleClaims.Where(x => x.Selected).ToList();
            foreach (var claim in SelectedClaims)
                await _roleManager.AddClaimAsync(role, new Claim(Helper.Permission,claim.Value));

            return RedirectToAction("Roles","Accounts");
        }
    }
}
