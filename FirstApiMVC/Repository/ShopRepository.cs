using FirstApiMVC.DbContexts;
using FirstApiMVC.DBContexts.Models;
using FirstApiMVC.DTO;
using FirstApiMVC.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Formats.Asn1;

namespace FirstApiMVC.Repository
{
    public class ShopRepository : IShopRepository
    {
        private readonly ShopDbContext _context;
        public ShopRepository(ShopDbContext context)
        {
            _context = context;
        }
        // ---------------------------------Multiple  item List ---------------------------------------
        public async Task<string> CreateItems(ItemListDto itemListDto)
        {
            var messages = new List<string>();
            foreach (var item in itemListDto.Items)
            {
                var result = await CreateOrUpdateItem(item);
                messages.Add(result);
            }
            return string.Join("; ", messages);
        }
        // --------------------------------- Create item ---------------------------------------
        private async Task<string> CreateOrUpdateItem(ItemDto item)
        {
            string message = "";
            try
            {
                var data = await _context.Items.FirstOrDefaultAsync(e => e.ItemId == item.ItemId);
                if (data != null)
                {
                    var duplicate = await _context.Items
                        .FirstOrDefaultAsync(e => e.ItemName.ToLower().Trim() == item.ItemName.ToLower().Trim() && e.ItemId != item.ItemId);
                    if (duplicate != null)
                    {
                        throw new Exception($"Item with name '{item.ItemName}' already exists.");
                    }

                    data.ItemName = item.ItemName;
                    data.NumStockQuantity = item.NumStockQuantity;
                    data.IsActive = item.IsActive;

                    _context.Items.Update(data);
                    message = $"Item '{item.ItemName}' updated successfully.";
                }
                else
                {
                    var duplicate = await _context.Items
                        .FirstOrDefaultAsync(e => e.ItemName.ToLower().Trim() == item.ItemName.ToLower().Trim());
                    if (duplicate != null)
                    {
                        throw new Exception($"Item with name '{item.ItemName}' already exists.");
                    }

                    var newItem = new Item
                    {
                        ItemName = item.ItemName,
                        NumStockQuantity = item.NumStockQuantity,
                        IsActive = item.IsActive
                    };

                    await _context.Items.AddAsync(newItem);
                    message = $"Item '{item.ItemName}' created successfully.";
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                message = $"Error: {ex.Message}";
            }

            return message;
        }
        // ---------------------------------Update list---------------------------------------
        public async Task<string> UpdateItems(List<ItemDto> items)
        {
            string message = "Items updated successfully";
            try
            {
                foreach (var itemDto in items)
                {
                    var item = await _context.Items.Where(e => e.ItemId == itemDto.ItemId).FirstOrDefaultAsync();
                    if (item == null)
                    {
                        throw new Exception($"Item with ID {itemDto.ItemId} not found");
                    }

                    var duplicateItem = await _context.Items
                        .Where(e => e.ItemName.ToLower().Trim() == itemDto.ItemName.ToLower().Trim() && e.ItemId != itemDto.ItemId)
                        .FirstOrDefaultAsync();
                    if (duplicateItem != null)
                    {
                        throw new Exception($"Item with name {itemDto.ItemName} already exists");
                    }

                    item.ItemName = itemDto.ItemName;
                    item.NumStockQuantity = itemDto.NumStockQuantity;
                    item.IsActive = itemDto.IsActive;

                    _context.Items.Update(item);
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating items: {ex.Message}");
            }

            return message;
        }

        // ---------------------------------Partner Details ---------------------------------------

        public async Task<string> CreatePartner(PartnerDto _partnerdto)
        {
            try
            {

                if (string.IsNullOrEmpty(_partnerdto.PartnerName))
                {
                    throw new ArgumentException("Partner Name can't empty!");
                }
                if (string.IsNullOrEmpty(_partnerdto.PartnerTypeName))
                {
                    throw new ArgumentException("Partner Type can't empty!");
                }

                var partnerType = await _context.PartnerTypes.FirstOrDefaultAsync(p => p.PartnerTypeName.ToLower() == _partnerdto.PartnerTypeName.ToLower());
                if (partnerType == null)
                {

                    partnerType = new PartnerType
                    {
                        PartnerTypeName = _partnerdto.PartnerTypeName,
                        IsActive = _partnerdto.IsActive,

                    };
                    await _context.PartnerTypes.AddAsync(partnerType);
                    await _context.SaveChangesAsync();
                }

                var partner = new Partner
                {
                    PartnerName = _partnerdto.PartnerName,
                    PartnerTypeId = partnerType.PartnerTypeId,
                    IsActive = _partnerdto.IsActive,
                };



                await _context.Partners.AddAsync(partner);
                await _context.SaveChangesAsync();

                return $"Partner Name '{_partnerdto.PartnerName}'& Partner Type '{_partnerdto.PartnerTypeName}' created successfully.";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> PurchaseProduct(PurchaseDto _purchasedto)
        {

            try
            {
                var purchase = new Purchase
                {
                    SupplierId=_purchasedto.SupplierId,
                    PurchaseDate=_purchasedto.PurchaseDate,
                    //IsActive=_purchasedto.IsActive,
                };
                await _context.Purchases.AddAsync(purchase);
                await _context.SaveChangesAsync();

                foreach(var _pr in _purchasedto.PurchaseDetails)
                {

                }



            }catch(Exception e)
            {
                return e.Message;
            }

        }



    }
}


   


