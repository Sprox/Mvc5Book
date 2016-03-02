using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public int PageSize = 1;
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public ViewResult List(string category, int page = 1)
        {
            ProductsListViewModel model = new ProductsListViewModel
            {
                Products =
                    _productRepository.Products.Where(p=>p.Category == null || p.Category == category).OrderBy(p => p.ProductID).Skip((page - 1)*PageSize).Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    TotalItems = _productRepository.Products.Count(),
                    ItemsPerPage = PageSize
                },
                CurrentCategory = category
                
                
            };
            return View(model);
        }

   
    }
}