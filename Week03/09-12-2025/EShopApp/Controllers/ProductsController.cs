using System.Text.Json;
using System.Threading.Tasks;
using EShopApp.Models;
using EShopApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace EShopApp.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService productService, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _logger = logger;
        }


        // GET: api/products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            var products = await _productService.GetAllAsync();
            return Ok(products);
        }

        // GET: api/products/5
        [HttpGet("{id}", Name = "GetProductById")]
        public async Task<ActionResult<Product>> GetById([FromRoute] int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product is null)
            {
                return NotFound(new { message = $"{id} id'li ürün bulunamadı!" });
            }
            return Ok(product);
        }


        // GET: api/products/low-stock?threshold=10
        [HttpGet("low-stock")]
        public async Task<ActionResult<IEnumerable<Product>>> GetLowStockProducts([FromQuery] int threshold = 10)
        {
            var products = await _productService.GetLowStockProductsAsync(threshold);
            if (products.Count == 0)
            {
                return NotFound(new { message = $"Stok miktarı {threshold} değerinden az olan ürün bulunamamıştır." });
            }
            return Ok(products);
        }


        // GET: api/products/by-category/telefon
        [HttpGet("by-category/{category:alpha}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(string category)
        {
            var products = await _productService.GetProductsByCategoryAsync(category);
            if (products.Count == 0)
            {
                return NotFound(new { message = $"{category} kategorisinde hiç ürün bulunamadı!" });
            }
            return Ok(products);
        }

        // GET: api/products/3/stock-check?quantity=10
        [HttpGet("{id}/stock-check")]
        public async Task<ActionResult<object>> CheckStock(int id, [FromQuery] int quantity)
        {
            var isAvaliable = await _productService.CheckStockAvailabilityAsync(id, quantity);
            var product = await _productService.GetByIdAsync(id);
            if (product is null)
            {
                return NotFound(new { message = $"{id} id'li ürün bulunamadığı için stok kontrolü yapılamadı!" });
            }
            return Ok(
                new
                {
                    productId = id,
                    productName = product.Name,
                    currentStock = product.Stock,
                    requestedQuantity = quantity,
                    isAvaliable = isAvaliable,
                    message = isAvaliable ? "Stok Yeterli" : "Stok Yetersiz."
                }
            );
        }


        //         // POST: api/products
        //         [HttpPost]
        //         public ActionResult<Product> Create([FromBody] Product product)
        //         {

        //             if (product is null)
        //             {
        //                 return BadRequest(new { message = "Ürün bilgisi boş olamaz!" });
        //             }
        //             if (string.IsNullOrWhiteSpace(product.Name))
        //             {
        //                 return BadRequest(new { message = "Ürün adı zorunludur!" });
        //             }
        //             if (product.Price <= 0)
        //             {
        //                 return BadRequest(new { message = "Ürün fiyatı 0'dan büyük olmalıdır!" });
        //             }
        //             if (product.Stock < 0)
        //             {
        //                 return BadRequest(new { message = "Stok miktarı negatif olamaz" });
        //             }
        //             var newProduct = _productService.Add(product);

        //             return CreatedAtRoute("GetProductById", new { id = newProduct.Id }, newProduct);
        //         }


        //         // PUT: api/products/4
        //         [HttpPut("{id}")]
        //         public ActionResult<Product> Update(int id, [FromBody] Product? product)
        //         {
        //             if (product is null)
        //             {
        //                 return BadRequest(new { message = "Ürün bilgisi boş olamaz!" });
        //             }

        //             if (id != product.Id)
        //             {
        //                 return BadRequest(new { message = "URL'deki id değeri ile body'deki id değeri eşleşmiyor!" });
        //             }

        //             if (string.IsNullOrWhiteSpace(product.Name))
        //             {
        //                 return BadRequest(new { message = "Ürün adı zorunludur!" });
        //             }
        //             if (product.Price <= 0)
        //             {
        //                 return BadRequest(new { message = "Ürün fiyatı 0'dan büyük olmalıdır!" });
        //             }
        //             if (product.Stock < 0)
        //             {
        //                 return BadRequest(new { message = "Stok miktarı negatif olamaz" });
        //             }
        //             var updatedProduct = _productService.Update(id, product);
        //             if (updatedProduct is null)
        //             {
        //                 return NotFound(new { message = $"{id} id'li ürün bulunamadığı için güncelleme işlemi tamamlanamadı!" });
        //             }
        //             return Ok(updatedProduct);
        //         }


        //         // DELETE: api/products/5
        //         [HttpDelete("{id}")]
        //         public IActionResult Delete(int id)
        //         {
        //             var isSuccess = _productService.Delete(id);
        //             if (!isSuccess)
        //             {
        //                 return NotFound(new { message = $"{id} id'li ürün bulunamadığı için silme işlemi tamamlanamadı!" });
        //             }
        //             return NoContent();
        //         }


        //         // GET: api/products/filter?minStock=5&minPrice=10...
        //         [HttpGet("filter")]
        //         public ActionResult<IEnumerable<Product>> Filter(
        //             [FromQuery] string? category,
        //             [FromQuery] decimal? minPrice,
        //             [FromQuery] decimal? maxPrice,
        //             [FromQuery] int? minStock
        //             )
        //         {
        //             var products = _productService.GetAll();
        //             if (!string.IsNullOrEmpty(category))
        //             {
        //                 products = products.Where(p => p.Category == category).ToList();
        //             }

        //             if (minPrice.HasValue)
        //             {
        //                 products = products.Where(p => p.Price >= minPrice.Value).ToList();
        //             }

        //             if (maxPrice.HasValue)
        //             {
        //                 products = products.Where(p => p.Price <= maxPrice.Value).ToList();
        //             }

        //             if (minStock.HasValue)
        //             {
        //                 products = products.Where(p => p.Stock >= minStock.Value).ToList();
        //             }
        //             if (products.Count == 0)
        //             {
        //                 return NotFound(new { message = "Belirtilen kriterlere uygun hiç ürün bulunamadı!" });
        //             }
        //             return Ok(products);
        //         }

        //         // UPDATE: api/products/4/stock
        //        [HttpPatch("{id}/stock")]
        //        public ActionResult<Product> UpdateStock(int id, [FromBody] StockUpdateRequest stockUpdateRequest)
        //        {
        //             try
        //             {
        //                 var product = _productService.UpdateStock(id,stockUpdateRequest.QuantityChange);
        //                 if(product is null)
        //                 {
        //                     return NotFound(new {message=$"{id} id'li ürün bulunamadığı için stok güncellenemedi!"});
        //                 }
        //                 return Ok(
        //                     new {
        //                         productId = id,
        //                         productName=product.Name,
        //                         oldStock=product.Stock - stockUpdateRequest.QuantityChange,
        //                         newStock=product.Stock,
        //                         change=stockUpdateRequest.QuantityChange
        //                     }
        //                 );
        //             }
        //             catch (InvalidOperationException ex)
        //             {
        //                 return BadRequest(new {message = $"Hata: {ex.Message}"});
        //             }
        //         } 


    }
}

