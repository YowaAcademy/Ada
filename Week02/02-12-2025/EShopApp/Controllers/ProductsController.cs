using EShopApp.Models;
using EShopApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShopApp.Controllers
{

    // [Route("api/[controller]/[action]")]     
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _productService.GetAll();

            return Ok(products);
        }

        [HttpGet("low-stock")]
        public IActionResult GetLowStockProducts([FromQuery] int threshold = 20)
        {
            var products = _productService.GetLowStockProducts(threshold);

            return Ok(products);
        }

        [HttpGet("by-category/{category}")]
        public IActionResult GetProductsByCategory(string category)
        {
            var products = _productService.GetProductsByCategory(category);
            if (products.Count == 0)
            {
                return NotFound(new { message = $"{category} kategorisinde hiç ürün bulunamadı!" });
            }
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _productService.GetById(id);
            if (product is null)
            {
                return NotFound(new { message = $"{id} id'li ürün bulunamadı!" });
            }
            return Ok(product);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Product? product)
        {
            if (product is null)
            {
                return BadRequest(new { message = "Ürün bilgisi boş olamaz!" });
            }
            if (string.IsNullOrWhiteSpace(product.Name))
            {
                return BadRequest(new { message = "Ürün adı zorunludur!" });
            }
            if (product.Price <= 0)
            {
                return BadRequest(new { message = "Ürün fiyatı 0'dan büyük olmalıdır!" });
            }
            if (product.Stock < 0)
            {
                return BadRequest(new { message = "Stok miktarı negatif olamaz" });
            }
            var newProduct = _productService.Add(product);

            return CreatedAtAction(nameof(GetById), new { id = newProduct.Id }, newProduct);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Product? product)
        {
            if (product is null)
            {
                return BadRequest(new { message = "Ürün bilgisi boş olamaz!" });
            }

            if (id != product.Id)
            {
                return BadRequest(new { message = "URL'deki id değeri ile body'deki id değeri eşleşmiyor!" });
            }

            if (string.IsNullOrWhiteSpace(product.Name))
            {
                return BadRequest(new { message = "Ürün adı zorunludur!" });
            }
            if (product.Price <= 0)
            {
                return BadRequest(new { message = "Ürün fiyatı 0'dan büyük olmalıdır!" });
            }
            if (product.Stock < 0)
            {
                return BadRequest(new { message = "Stok miktarı negatif olamaz" });
            }
            var updatedProduct = _productService.Update(id, product);
            if (updatedProduct is null)
            {
                return NotFound(new { message = $"{id} id'li ürün bulunamadığı için güncelleme işlemi tamamlanamadı!" });
            }
            return Ok(updatedProduct);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var isSuccess = _productService.Delete(id);
            if (!isSuccess)
            {
                return NotFound(new { message = $"{id} id'li ürün bulunamadığı için silme işlemi tamamlanamadı!" });
            }
            return NoContent();
        }


        [HttpGet("filter")]
        public IActionResult Filter(
            [FromQuery] string? category,
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice,
            [FromQuery] int? minStock
            )
        {
            
        }
    }
}
