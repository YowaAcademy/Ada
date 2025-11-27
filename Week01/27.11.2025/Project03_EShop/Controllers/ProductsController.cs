using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project03_EShop.Models;
using Project03_EShop.Services;

namespace Project03_EShop.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        

        /// <summary>
        /// Tüm ürünleri getirir(stok durumu ile birlikte)
        /// </summary>
        /// <returns>Ürün Listesi</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _productService.GetAll();
            return Ok(products);
        }


        /// <summary>
        /// ID'ye göre ürün bilgisini getirir.
        /// </summary>
        /// <param name="id">Getirilmek istenen ürünün id'si</param>
        /// <returns>Ürün</returns>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _productService.GetById(id);
            if(product is null)
            {
                return NotFound();
            }
            return Ok(product);
        }


        /// <summary>
        /// Düşük stoklu ürünleri getirir
        /// </summary>
        /// <param name="threshold">Stok eşik değeri(varsayılan:10)</param>
        /// <returns>Düşük stoklu ürün listesi</returns>
        [HttpGet("low-stock")]    // api/products/low-stock?threshold=8
        public IActionResult GetLowStockProducts([FromQuery] int threshold=10)
        {
            var products = _productService.GetLowStockProducts(threshold);
            return Ok(products);
        }



        [HttpPost]
        public IActionResult AddProduct([FromBody] Product product)
        {
            _productService.Add(product);
            return Ok(product);
        }
    }
}
