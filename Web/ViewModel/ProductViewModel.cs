using Core.Poco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModel
{
    public class ProductViewModel
    {
        public ProductPoco Product { get; set; }

        public List<string> CategoryNameList { get; set; }

        public List<CategoryPoco> Categories { get; set; }

        public List<BrandPoco> Brands { get; set; }

        public List<ProductCategoryMapPoco> MapList { get; set; }
    }
}