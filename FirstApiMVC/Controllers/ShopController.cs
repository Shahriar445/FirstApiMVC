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
        public async Task<IActionResult> CreateItem([FromForm] ItemCreateDto itemDto)
        {
            try
            {
                string imageUrl = null;
                if (itemDto.ImageFile != null && itemDto.ImageFile.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(itemDto.ImageFile.FileName);
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await itemDto.ImageFile.CopyToAsync(stream);
                    }
                    imageUrl = $"/images{fileName}";
                }

                var itemDtos = new ItemDto
                {
                    ItemName = itemDto.ItemName,
                    NumStockQuantity = itemDto.NumStockQuantity,
                    IsActive = itemDto.IsActive,
                    ImageUrl = imageUrl
                };

                var result = await _shopRepo.CreateItem(itemDtos, imageUrl);
                return StatusCode(StatusCodes.Status201Created, result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }



        [HttpPost("/CreateItems")]
        public async Task<IActionResult> CreateItems(List<ItemDto> itemDtos)
        {
            try
            {
                var itemListDto = new ItemListDto { Items = itemDtos };
                var result = await _shopRepo.CreateItems(itemListDto);
                return StatusCode(StatusCodes.Status201Created, result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // ---------------------------------PartnerType---------------------------------------
        /* [HttpPost("/CreatePartnerType")]  not implement now 
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

       


        [HttpGet("/GetAllItems")]
        public async Task<IActionResult> GetAllItems(  )
        {
            try
            {
                var items = await _shopRepo.GetAllItems();
                return Ok(items);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        // ---------------------------------Update items--------------------------------------------------- 
        [HttpPut("/UpdateItems")]
        public async Task<IActionResult> UpdateItems( List<ItemDto> items)
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


