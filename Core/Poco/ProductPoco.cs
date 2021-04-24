using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Poco
{
    public class ProductPoco : Product
    {
        public List<string> CategoryName { get; set; }

        public string BrandName { get; set; }
    }
}
