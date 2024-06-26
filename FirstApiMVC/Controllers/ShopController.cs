﻿
using FirstApiMVC.DBContexts.Models;
using FirstApiMVC.DTO;
using FirstApiMVC.IRepository;
using FirstApiMVC.jwttoken;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;


namespace FirstApiMVC.Controllers
{


    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private static IShopRepository _shopRepo;
        private readonly ILogger<ShopController> _logger;
        private readonly TokenService _tokenService;

        public ShopController(ILogger<ShopController> logger, IShopRepository shopRepo, TokenService tokenService)
        {
            _logger = logger;
            _shopRepo = shopRepo;
            _tokenService = tokenService;
        }
        //----------------------------------------------REgister & login api --------------------------------------
        [HttpPost("/Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequest)
        {
            try
            {
                var existingUser = await _shopRepo.GetUserByUsernameAsync(registerRequest.Username);
                if (existingUser != null)
                {
                    return BadRequest("Username already exists");
                }

                var user = new User
                {
                    Username = registerRequest.Username,
                    Password = registerRequest.Password 
                };

                var createdUser = await _shopRepo.CreateUserAsync(user);
                return StatusCode(StatusCodes.Status201Created, createdUser);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [HttpPost("/Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto _logindto)
        {
            try
            {
                var user = await _shopRepo.GetUserByUsernameAsync(_logindto.Username);
                if (user != null && user.Password == _logindto.Password) // Compare plain text passwords
                {
                    var token = _tokenService.GenerateToken(user.UserId.ToString());
                    return Ok(new { Token = token });
                }

                return Unauthorized("Invalid username or password");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        //----------------------------------------------Create Items --------------------------------------
        [HttpPost("/CreateItem")]
        public async Task<IActionResult> CreateItem([FromForm] ItemCreateDto itemDto)
        {
            try
            {
                string imageUrl = null;
                string fileUrl = null;
                if (itemDto.ImageFile != null && itemDto.ImageFile.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(itemDto.ImageFile.FileName);
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await itemDto.ImageFile.CopyToAsync(stream);
                    }
                    imageUrl = $"/images/{fileName}";
                }
                if (itemDto.File != null && itemDto.File.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(itemDto.File.FileName);
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await itemDto.File.CopyToAsync(stream);
                    }
                    fileUrl = $"/uploads/{fileName}";
                }

                var itemDtos = new ItemDto
                {
                    ItemName = itemDto.ItemName,
                    NumStockQuantity = itemDto.NumStockQuantity,
                    IsActive = itemDto.IsActive,
                    FileUrl = fileUrl,
                    ImageUrl = imageUrl
                };

                var result = await _shopRepo.CreateItem(itemDtos, imageUrl, fileUrl);
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
        public async Task<IActionResult> GetAllItems()
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
        public async Task<IActionResult> UpdateItems(List<ItemDto> items)
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
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("/Sales")]
        public async Task<IActionResult> SalesProduct(SaleDto _sale)
        {
            try
            {
                var result = await _shopRepo.SalesProduct(_sale);
                return StatusCode(StatusCodes.Status201Created, result);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpGet("/DailyPurchase")]

        public async Task<IActionResult> GetDailyPurchaseReport(DateTime startDate, DateTime endDate)
        {
            try
            {
                var result = await _shopRepo.GetDailyPurchaseReport(startDate, endDate);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }


        [HttpGet("/GetItem")]
        public async Task<IActionResult> GetItem(int itemId)
        {
            try
            {
                var item = await _shopRepo.GetItemById(itemId);
                if (item == null)
                {
                    return NotFound($"Item with ID {itemId} not found.");
                }
                return Ok(item);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


    }

}


