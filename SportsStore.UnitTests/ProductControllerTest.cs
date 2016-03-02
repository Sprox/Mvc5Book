using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using SportsStore.WebUI.HtmlHelpers;
using SportsStore.WebUI.Models;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class ProductControllerTest
    {
        [TestMethod]
        public void Can_Paginate()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(p => p.Products).Returns(new Product[]
            {
                new Product { ProductID = 1, Name = "P1" },
                new Product { ProductID = 2, Name = "P2" },
                new Product { ProductID = 3, Name = "P3" },
                new Product { ProductID = 4, Name = "P4" },
                new Product { ProductID = 5, Name = "P5" },
                new Product { ProductID = 6, Name = "P6" }
            });

            ProductController productController = new ProductController(mock.Object);
            productController.PageSize = 3;

            var result = (ProductsListViewModel)productController.List(null,2).Model;

            var enumerable = result.Products as Product[] ?? result.Products.ToArray();
            Assert.IsTrue(enumerable.Count() == 3);
            Assert.AreEqual(enumerable.ElementAt(0).Name, "P4");
        }

        [TestMethod]
        public void Can_Generate_Page_Links()
        {
            HtmlHelper myHelper = null;

            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            Func<int, string> pageUrlDelegate = i => "Strona" + i;

            
            var result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            Assert.IsTrue(result.ToString().Contains("Strona1"));
            Assert.IsTrue(result.ToString().Contains("Strona2"));
            Assert.IsTrue(result.ToString().Contains("Strona3"));
        }

     

       
    }
}
