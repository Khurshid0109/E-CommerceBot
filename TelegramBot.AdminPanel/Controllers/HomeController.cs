using Microsoft.AspNetCore.Mvc;
using TelegramBot.Service.DTOs.Products;
using TelegramBot.Service.Interfaces;

namespace TelegramBot.AdminPanel.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _productService.RetrieveAllAsync();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Details(long id)
        {
            var res = await _productService.RetrieveByIdAsync(id);

            return View(res);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductForCreateDto dto)
        {
            if (ModelState.IsValid)
            { 
                var product=await _productService.AddAsync(dto);
                return RedirectToAction("details", new { id = product.Id });
            }
            return View();
        }
    }
}
