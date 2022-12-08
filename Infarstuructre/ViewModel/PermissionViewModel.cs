using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infarstuructre.ViewModel
{
    public class PermissionViewModel
    {
        public string RoleId { get; set; } = string.Empty;

        public string RoleName { get; set; } = string.Empty;

        public List<RoleClaimsViewModel> RoleClaims { get; set; } = new List<RoleClaimsViewModel>();

    }

    public class RoleClaimsViewModel
    {
        public string Value { get; set; } = string.Empty;

        public bool Selected { get; set; }
    }
}
