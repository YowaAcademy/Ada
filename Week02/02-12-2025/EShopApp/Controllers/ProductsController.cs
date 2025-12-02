using EShopApp.Models;
using EShopApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EShopApp.Controllers
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

        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _productService.GetAll();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _productService.GetById(id);
            if(product is null)
            {
                return NotFound(new {message=$"{id} id'li ürün bulunamadı!"});
            }
            return Ok(product);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Product product)
        {
            if(product is null)
            {
                return BadRequest(new {message="Ürün bilgisi boş olamaz!"});
            }
            if(string.IsNullOrWhiteSpace(product.Name))
            {
                return BadRequest(new {message = "Ürün adı zorunludur!"});
            }
            if(product.Price<=0)
            {
                return BadRequest( new {message= "Ürün fiyatı 0'dan büyük olmalıdır!"});
            }
            if(product.Stock<0)
            {
                return BadRequest( new { message="Stok miktarı negatif olamaz"});
            }
            var newProduct = _productService.Add(product);
            
            return CreatedAtAction(nameof(GetById), new {id=newProduct.Id}, newProduct);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id)
        {
            return null!;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return null!;
        }
    }
}
