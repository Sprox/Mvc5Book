using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Moq;
using Ninject;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Concrete;
using SportsStore.Domain.Entities;

namespace SportsStore.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel kernel;

        public NinjectDependencyResolver(IKernel kernel)
        {
            this.kernel = kernel;
            AddBindings();
        }


        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            //Mock<IProductRepository> mock = new Mock<IProductRepository>();
            //mock.Setup(m => m.Products).Returns(new List<Product>
            //{
            //    new Product {Name = "Kajak", Category = "Sport wodne", Price = 275M},
            //    new Product {Name = "Kamizelka ratunkowa", Category = "Sporty wodne", Price = 48.95M},
            //    new Product {Name = "Piłka nożna", Category = "Piłka nożna", Price = 34.95M}
            //});

            kernel.Bind<IProductRepository>().To<EFProductRepository>();
        }
    }
}