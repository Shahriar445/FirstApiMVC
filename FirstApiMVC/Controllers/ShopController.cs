using FirstApiMVC.DBContexts.Models;
using FirstApiMVC.DTO;
using FirstApiMVC.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Formats.Asn1;


namespace FirstApiMVC.Controllers
{


    [Route("api/[controller]/[action]")]
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
        [HttpPost("/CreateItem")]
        public async Task<IActionResult> CreateItem(ItemDto itemdto)
        {
            try
            {

                var result = await _shopRepo.CreateItem(itemdto);
                return StatusCode(StatusCodes.Status201Created, result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }






        [HttpPost("/CreateItems")]
        public async Task<IActionResult> CreateItems( ItemListDto itemListDto)
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
       /* [HttpPost("/CreatePartnerType")]
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

        }*/

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
        
        // ---------------------------------Partner Detials  --------------------------------------------------- 
       
        [HttpPost("/CreatePartner")]
        public async Task<IActionResult> CreatePartner(PartnerDto partnerDto)
        {
            try
            {
                var result = await _shopRepo.CreatePartner(partnerDto);
                return StatusCode(StatusCodes.Status201Created, result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("/Purchase")]
        public async Task<IActionResult> PurchaseProduct(PurchaseDto _purchase)
        {
            try
            {
                var result = await _shopRepo.PurchaseProduct(_purchase);
                return StatusCode(StatusCodes.Status201Created, result);
            }catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,ex.Message);
            }
        }

        [HttpPost("/Sales")]
        public async Task<IActionResult> SalesProduct(SaleDto _sale)
        {
            try
            {
                var result = await _shopRepo.SalesProduct(_sale);
                return StatusCode(StatusCodes.Status201Created,result);

            }
            catch(Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("/DailyPurchase")]
        
        public async Task <IActionResult> GetDailyPurchaseReport(DateTime startDate, DateTime endDate)
        {
            try
            {
                var result = await _shopRepo.GetDailyPurchaseReport(startDate,  endDate);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

    }

}


