using FirstApiMVC.DBContexts.Models;
using FirstApiMVC.DTO;
using FirstApiMVC.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace FirstApiMVC.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private static IShopRepository _shopRepo;
        private readonly ILogger<ShopController> _logger;

        public ShopController(ILogger<ShopController> logger, IShopRepository shopRepo)
        {
            _logger = logger;
            _shopRepo = shopRepo;
        }
       
        [HttpPost("/CreateItem")]
        public async Task<IActionResult> CreateItem(ItemDto item)
        {
            try
            {
                var createItem = await _shopRepo.CreateItem(item);
                return StatusCode(StatusCodes.Status201Created, createItem);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }
        [HttpPut("/Update")]
        public async Task<IActionResult> UpdateItem(int Id ,ItemDto item)
        {
            try
            {

                var createdItem = await _shopRepo.UpdateItem(Id, item);

                if (createdItem == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Item with id :" + Id + " was not found");
                }
                else  
                {
                    return Ok(createdItem);
                }


            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

    }

}
