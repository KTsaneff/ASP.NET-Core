using Microsoft.AspNetCore.Mvc;
using MVCIntroDemo.Models;

namespace MVCIntroDemo.Controllers
{
    public class ProductController : Controller
    {
        private IEnumerable<ProductViewModel> _products;

        public ProductController()
        {
            _products = ProductList();
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult All()
        {
            return View(_products);
        }

        public IActionResult ById(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return BadRequest();
            }
            return View(product);
        }

        private List<ProductViewModel> ProductList()
        {
            List<ProductViewModel> hardcodedProducts = new List<ProductViewModel>();

            hardcodedProducts.Add(new ProductViewModel { Id = 1, Name = "Cheese", Price = 12.75 });
            hardcodedProducts.Add(new ProductViewModel { Id = 2, Name = "Milk", Price = 3.50 });
            hardcodedProducts.Add(new ProductViewModel { Id = 3, Name = "Bread", Price = 2.75 });
            hardcodedProducts.Add(new ProductViewModel { Id = 4, Name = "Butter", Price = 5.25 });
            hardcodedProducts.Add(new ProductViewModel { Id = 5, Name = "Eggs", Price = 1.50 });
            
            return hardcodedProducts;
        }
    }
}
