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

        //----------------------------------------------Create Items --------------------------------------
        [HttpPost("/CreateItems")]
        public async Task<IActionResult> CreateItems(ItemListDto itemListDto)
        {
            try
            {
                var result = await _shopRepo.CreateItems(itemListDto);
                return StatusCode(StatusCodes.Status201Created, result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        
        // ---------------------------------PartnerType---------------------------------------
        [HttpPost("/CreatePartnerType")]
        public async Task<IActionResult> CreatePartnerType(PartnerTypeDto partnertype)
        {
            try
            {
                var createPartner = await _shopRepo.CreatePartnerType(partnertype);
                return StatusCode(StatusCodes.Status201Created, createPartner);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }

        // ---------------------------------Update items--------------------------------------------------- 
        [HttpPut("/UpdateItems")]
        public async Task<IActionResult> UpdateItems([FromBody] List<ItemDto> items)
        {
            try
            {
                var message = await _shopRepo.UpdateItems(items);
                return Ok(message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }



    }

}


