using Core.Helper;
using Core.Poco;
using Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.ViewModel;

namespace Web.Controllers
{
    public class BrandController : Controller
    {
        IDatabaseConnectionFactory databaseConnectionFactory;
        public BrandController()
        {
            databaseConnectionFactory = new DatabaseConnectionFactory();
        }
        public ActionResult List()
        {
            BrandViewModel model = new BrandViewModel();
            model.Brands = new BrandRepository(databaseConnectionFactory).GetAllValues().ToList();
            return View(model);
        }

        [HttpGet]
        public ActionResult Add()
        {           
            return View();
        }

        [HttpPost]
        public ActionResult Add(BrandPoco obj)
        {
            var control = new BrandRepository(databaseConnectionFactory).Insert(obj);
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            BrandViewModel model = new BrandViewModel();
            model.Brand = new BrandRepository(databaseConnectionFactory).GetById(id);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(BrandPoco obj)
        {
            var control = new BrandRepository(databaseConnectionFactory).AddOrUpdate(obj);

            return RedirectToAction("List");
        }

        public ActionResult Delete(int id)
        {
            var control = new BrandRepository(databaseConnectionFactory).Delete(id);

            return RedirectToAction("List");
        }
    }
}