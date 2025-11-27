using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Project02_WebAPI.Controllers
{
    [Route("api/value")]   //   /api/value
    [ApiController]
    public class ValueController : ControllerBase
    {
        [HttpGet]  //    /api/value
        public string GetAll() 
        {
            return "Ürünler listelendi";
        }


        [HttpGet("{id}")]  //    /api/value/5
        // [Route("{id}")]  //    /api/value/3
        public string Get(int id)
        {
            return $"{id} id'li ürün getirildi";
        }

        [HttpPost]  //    /api/value
        public string Add()
        {
            return "Kayıt eklendi";
        }


        [HttpPut]  //    /api/value
        public string Update()
        {
            return "Kayıt güncellendi";
        }


        [HttpDelete]  //    /api/value
        public string Remove()
        {
            return "Kayıt silindi";
        }
    }
}
