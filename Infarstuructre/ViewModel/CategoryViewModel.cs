using Domin.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infarstuructre.ViewModel
{
    public class CategoryViewModel
    {
        public List<Category> Categories { get; set; }

        public List<LogCategory> LogCategories { get; set; }

        public Category NewCategory { get; set; }
    }
}
