using Core.Poco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.ViewModel
{
    public class BrandViewModel
    {
        public BrandPoco Brand { get; set; }

        public List<BrandPoco> Brands { get; set; }
    }
}