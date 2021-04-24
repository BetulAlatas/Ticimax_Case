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
    public class ProductController : Controller
    {
        IDatabaseConnectionFactory databaseConnectionFactory;
        public ProductController()
        {
            databaseConnectionFactory = new DatabaseConnectionFactory();
        }

        public ActionResult List()
        {
            List<ProductViewModel> model = new List<ProductViewModel>();
            var productList = new ProductRepository(databaseConnectionFactory).GetAllValues().ToList();
            var repo = new ProductCategoryMapRepository(databaseConnectionFactory);
            foreach (var item in productList)
            {
                model.Add(new ProductViewModel { Product = item, CategoryNameList = repo.GetByProductId(item.Id).ToList() });
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Add()
        {
            ProductViewModel model = new ProductViewModel();
            model.Categories = new CategoryRepository(databaseConnectionFactory).GetAllValues().ToList();
            model.Brands = new BrandRepository(databaseConnectionFactory).GetAllValues().ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(ProductPoco obj, int[] categoryId)
        {
            IDatabaseConnectionFactory databaseConnectionFactory = new DatabaseConnectionFactory();
            var productId = new ProductRepository(databaseConnectionFactory).Insert(obj);

            ProductCategoryMapRepository repository = new ProductCategoryMapRepository(databaseConnectionFactory);
            foreach (var item in categoryId)
            {
                ProductCategoryMapPoco objMap = new ProductCategoryMapPoco();
                objMap.ProductId = productId;
                objMap.CategoryId = item;

                var control = repository.Insert(objMap);
            }

            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            IDatabaseConnectionFactory databaseConnectionFactory = new DatabaseConnectionFactory();
            ProductViewModel model = new ProductViewModel();
            model.Product = new ProductRepository(databaseConnectionFactory).GetById(id);

            model.Categories = new CategoryRepository(databaseConnectionFactory).GetAllValues().ToList();
            model.Brands = new BrandRepository(databaseConnectionFactory).GetAllValues().ToList();
            model.MapList = new ProductCategoryMapRepository(databaseConnectionFactory).GetMany(x => x.ProductId == id).ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ProductPoco obj, int[] categoryId)
        {
            IDatabaseConnectionFactory databaseConnectionFactory = new DatabaseConnectionFactory();
            var control = new ProductRepository(databaseConnectionFactory).AddOrUpdate(obj);
            ProductCategoryMapRepository productCategoryMapRepository = new ProductCategoryMapRepository(databaseConnectionFactory);
            var productCategoryMaps = productCategoryMapRepository.GetMany(x => x.ProductId == obj.Id);
            foreach (var productCategoryMap in productCategoryMaps)
            {
                productCategoryMapRepository.Delete(productCategoryMap.Id);
            }

            foreach (int id in categoryId)
            {
                productCategoryMapRepository.Insert(new ProductCategoryMapPoco
                {
                    ProductId = obj.Id,
                    CategoryId = id
                });
            }

            return RedirectToAction("List");
        }

        public ActionResult Delete(int id)
        {
            IDatabaseConnectionFactory databaseConnectionFactory = new DatabaseConnectionFactory();
            var control = new ProductRepository(databaseConnectionFactory).Delete(id);

            return RedirectToAction("List");
        }
    }
}