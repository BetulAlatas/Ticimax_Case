using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Poco
{
    public class CategoryPoco : Categories
    {
        public string ParentName { get; set; }
    }
}
