using Core.Poco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Web.ViewModel
{
    public class CategoryViewModel
    {
        public CategoryPoco Category {get;set;}

        public List<CategoryPoco> Categories { get; set; }
    }
}