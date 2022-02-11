using Domin.Resource;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infarstuructre.ViewModel
{
    public class ChangePasswordViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessageResourceType = typeof(ResourceData),ErrorMessageResourceName ="Password")]
        [MinLength(5)]
        public string NewPassword { get; set; }


        [Required(ErrorMessageResourceType = typeof(ResourceData), ErrorMessageResourceName = "ComparePassword")]
        [Compare("NewPassword")]
        public string ComparePassword { get; set; }
    }
}
