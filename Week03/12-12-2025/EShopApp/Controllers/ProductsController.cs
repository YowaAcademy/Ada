using System.Text.Json;
using System.Threading.Tasks;
using EShopApp.Models;
using EShopApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        // GET: api/products/deleted
        [HttpGet("deleted")]
        public async Task<ActionResult<IEnumerable<Product>>> GetDeletedProducts()
        {
            var products = await _productService.GetDeletedProductsAsync();
            return Ok(products);
        }


        // GET: api/products/paged?pageNumber=1&pageSize=10
        [HttpGet("paged")]
        public async Task<ActionResult<IEnumerable<Product>>> GetPaged(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10
        )
        {
            var (products, totalCount) = await _productService.GetProductsPagedAsync(pageNumber, pageSize);

            var totalPages = Math.Ceiling((double)totalCount / pageSize);
            Response.Headers.Append("X-Pagination-TotalCount", totalCount.ToString());
            Response.Headers.Append("X-Pagination-PageSize", pageSize.ToString());
            Response.Headers.Append("X-Pagination-CurrentPage", pageNumber.ToString());
            Response.Headers.Append("X-Pagination-TotalPages", totalPages.ToString());



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


        // POST: api/products
        [HttpPost]
        public async Task<ActionResult<Product>> Create([FromBody] Product product)
        {
            try
            {
                var newProduct = await _productService.AddAsync(product);

                return CreatedAtRoute("GetProductById", new { id = newProduct.Id }, newProduct);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { error = ex.Message });
            }

        }


        // PUT: api/products/4
        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> Update(int id, [FromBody] Product? product)
        {
            try
            {
                var updatedProduct = await _productService.UpdateAsync(id, product!);

                return Ok(updatedProduct);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Conflict(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "BEKLENMEYEN HATA: " + ex.Message });
            }

        }


        // DELETE: api/products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var isSuccess = await _productService.DeleteAsync(id);
            if (!isSuccess)
            {
                return NotFound(new { message = $"{id} id'li ürün bulunamadığı için silme işlemi tamamlanamadı!" });
            }
            return NoContent();
        }



        // GET: api/products/filter?minStock=5&minPrice=10...
        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<Product>>> Filter([FromQuery] ProductQuery query)
        {
            var products = await _productService.FilterAsync(query);
            var totalCount = products.Count();
            Response.Headers.Append("X-Pagination-TotalCount", totalCount.ToString());
            return Ok(new { totalCount, products });
        }

        // UPDATE: api/products/4/stock
        [HttpPatch("{id}/stock")]
        public async Task<ActionResult<Product>> UpdateStock(int id, [FromBody] StockUpdateRequest stockUpdateRequest)
        {
            try
            {
                var product = await _productService.UpdateStockAsync(id, stockUpdateRequest.QuantityChange);
                return Ok(
                    new
                    {
                        productId = id,
                        productName = product!.Name,
                        oldStock = product.Stock - stockUpdateRequest.QuantityChange,
                        newStock = product.Stock,
                        change = stockUpdateRequest.QuantityChange
                    }
                );
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }


    }
}

