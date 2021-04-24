using Core.Helper;
using Core.Poco;
using Core.Repository;
using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.ViewModel;

namespace Web.Controllers
{
    public class CategoryController : Controller
    {
        IDatabaseConnectionFactory databaseConnectionFactory;
        public CategoryController()
        {
            databaseConnectionFactory = new DatabaseConnectionFactory();
        }

        public ActionResult List()
        {
            CategoryViewModel model = new CategoryViewModel();
            model.Categories = new CategoryRepository(databaseConnectionFactory).GetAllValues().ToList();
            return View(model);
        }

        [HttpGet]
        public ActionResult Add()
        {
            CategoryViewModel model = new CategoryViewModel();
            model.Categories = new CategoryRepository(databaseConnectionFactory).GetAllValues().ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(CategoryPoco obj)
        {
            var control = new CategoryRepository(databaseConnectionFactory).Insert(obj);
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            CategoryViewModel model = new CategoryViewModel();
            model.Categories = new CategoryRepository(databaseConnectionFactory).GetAllValues().ToList();
            model.Category = new CategoryRepository(databaseConnectionFactory).GetById(id);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(CategoryPoco obj)
        {
            var control = new CategoryRepository(databaseConnectionFactory).AddOrUpdate(obj);

            return RedirectToAction("List");
        }

        public ActionResult Delete(int id)
        {
            var control = new CategoryRepository(databaseConnectionFactory).Delete(id);

            return RedirectToAction("List");
        }
    }
}