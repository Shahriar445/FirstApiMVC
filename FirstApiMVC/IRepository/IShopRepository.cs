﻿using FirstApiMVC.DBContexts.Models;
using FirstApiMVC.DTO;

namespace FirstApiMVC.IRepository
{
    public interface IShopRepository
    {
        Task<string> CreateItem(ItemDto item,string ImageUrl,string FileUrl);
        
        Task<string> CreateItems(ItemListDto itemListDto);
        Task<string> UpdateItems(List<ItemDto> items);
       
        Task<string> CreatePartner(PartnerDto _partnerdto);
        Task<string> PurchaseProduct(PurchaseDto _purchasedto);
        Task<string> SalesProduct(SaleDto _saledto);
        Task<string>GetDailyPurchaseVsSalesReport(DailyReportDto _dailypurchasedto);
        Task<List<DailyPurchaseReportDto>> GetDailyPurchaseReport(DateTime startDate, DateTime endDate);
         Task<List<ItemDto>> GetAllItems();
        Task<User> GetUserByUsernameAsync(string username);
        Task<User> CreateUserAsync(User user);
        Task<string> GetItemById(int id);
    }
}

