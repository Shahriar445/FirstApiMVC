﻿using FirstApiMVC.DBContexts.Models;
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
       
        [HttpPost( Name = "CreateItem")]
        public async Task<IActionResult> CreateItem(Item item)
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

    }

}