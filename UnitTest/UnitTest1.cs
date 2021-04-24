using Core.Helper;
using Core.Repository;
using Data.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ProductRepository_GetById()
        {
            Mock<IDatabaseConnectionFactory> connectionFactoryMock = new Mock<IDatabaseConnectionFactory>();

            var products = new List<Product>
            {
               new Product { BrandId=1,Description="açıklama",DiscountPrice=10,Name="test ürünü",Price=100,Quantitiy=1,Status=true,Id=1 }
            };

            var db = new InMemoryDatabase();
            db.Insert(products);

            connectionFactoryMock.Setup(c => c.GetConnection()).Returns(db.OpenConnection());
            var result = new ProductRepository(connectionFactoryMock.Object).GetById(1);
            Assert.AreEqual(1, result.Id);
        }

        [TestMethod]
        public void ProductRepository_Get()
        {
            Mock<IDatabaseConnectionFactory> connectionFactoryMock = new Mock<IDatabaseConnectionFactory>();

            var products = new List<Product>
            {
               new Product { BrandId=1,Description="açıklama",DiscountPrice=10,Name="test ürünü",Price=100,Quantitiy=1,Status=true,Id=1 }
            };

            var db = new InMemoryDatabase();
            db.Insert(products);

            connectionFactoryMock.Setup(c => c.GetConnection()).Returns(db.OpenConnection());
            var result = new ProductRepository(connectionFactoryMock.Object).Get(x => x.Id == 2);
            Assert.AreEqual(null, result);
        }

        [TestMethod]
        public void ProductRepository_GetAllValues()
        {
            Mock<IDatabaseConnectionFactory> connectionFactoryMock = new Mock<IDatabaseConnectionFactory>();

            var products = new List<Product>
            {
               new Product { BrandId=1,Description="açıklama",DiscountPrice=10,Name="test ürünü",Price=100,Quantitiy=1,Status=true,Id=1 }
            };

            var brands = new List<Brands>
            {
                new Brands { Name = "Marka1", Status = true ,Id=1}
            };

            var db = new InMemoryDatabase();
            db.Insert(products);
            db.Insert(brands);

            connectionFactoryMock.Setup(c => c.GetConnection()).Returns(db.OpenConnection());
            var result = new ProductRepository(connectionFactoryMock.Object).GetAllValues();
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public void ProductRepository_Delete()
        {
            Mock<IDatabaseConnectionFactory> connectionFactoryMock = new Mock<IDatabaseConnectionFactory>();

            var products = new List<Product>
            {
               new Product { BrandId=1,Description="açıklama",DiscountPrice=10,Name="test ürünü",Price=100,Quantitiy=1,Status=true,Id=1 }
            };

            var db = new InMemoryDatabase();
            db.Insert(products);

            connectionFactoryMock.Setup(c => c.GetConnection()).Returns(db.OpenConnection());
            var result = new ProductRepository(connectionFactoryMock.Object).Delete(1);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void ProductRepository_GetMany()
        {
            Mock<IDatabaseConnectionFactory> connectionFactoryMock = new Mock<IDatabaseConnectionFactory>();

            var products = new List<Product>
            {
               new Product { BrandId=1,Description="açıklama",DiscountPrice=10,Name="test ürünü",Price=100,Quantitiy=1,Status=true,Id=1 }
            };

            var db = new InMemoryDatabase();
            db.Insert(products);

            connectionFactoryMock.Setup(c => c.GetConnection()).Returns(db.OpenConnection());
            var result = new ProductRepository(connectionFactoryMock.Object).GetMany(x => x.BrandId == 1);
            Assert.AreEqual(1, result.Count());
        }

    }
}
