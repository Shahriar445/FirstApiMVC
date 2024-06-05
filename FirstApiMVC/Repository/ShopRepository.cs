using FirstApiMVC.DbContexts;
using FirstApiMVC.DBContexts.Models;
using FirstApiMVC.DTO;
using FirstApiMVC.IRepository;
using Microsoft.AspNetCore.Http.HttpResults;
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

        // single items 
        public async Task<string> CreateItem(ItemDto item)
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

        // ---------------------------------Multiple  item  ---------------------------------------
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


                var supplier = await _context.Partners.FindAsync(_purchasedto.SupplierId);
                if (supplier == null)
                {
                    return $"Supplier with ID {_purchasedto.SupplierId} not found. or invalid supplier type.";
                }

                // for purchase create 

                var purchase = new Purchase
                {
                    SupplierId=_purchasedto.SupplierId,
                    PurchaseDate=_purchasedto.PurchaseDate,
                    IsActive=_purchasedto.IsActive,
                };
                await _context.Purchases.AddAsync(purchase);
                await _context.SaveChangesAsync();


                // Purchase Details 
                foreach (var _detail in _purchasedto.PurchaseDetails)
                {

                    // for item 
                    var item = await _context.Items.FindAsync(_detail.ItemId);
                    if (item != null)
                    {
                        item.NumStockQuantity+=_detail.ItemQuantity;
                        _context.Items.Update(item);
                    }
                    if (item == null)
                    {
                        return $"Item with ID {_detail.ItemId} not found.";
                    }
                    if (_detail.ItemQuantity <= 0)
                    {
                        return "Item quantity must be greater than zero.";
                    }

                    if (_detail.UnitPrice <= 0)
                    {
                        return "Unit price must be greater than zero.";
                    }

                    var purchaseDetail = new PurchaseDetail
                    {
                        PurchaseId =purchase.PurchaseId,
                        ItemId=_detail.ItemId,
                        UnitPrice=_detail.UnitPrice,
                        IsActive=_purchasedto.IsActive,
                        ItemQuantity=_detail.ItemQuantity
                    };

                    await _context.PurchaseDetails.AddAsync(purchaseDetail);
                }

                await _context.SaveChangesAsync();
                return "Purchase Create Success";


            }
            catch (Exception e)
            {
                return e.Message;
            }

        }

        public async Task<string> SalesProduct(SaleDto _saledto)
        {

            try
            {

                var customer = await _context.Partners.FindAsync(_saledto.CustomerId);
                if (customer == null || customer.PartnerTypeId != 1) // Assuming 1 is the PartnerTypeId for customers
                {
                    return "Customer not found or invalid customer type.";
                }

                // Sales 

                var salerecord = new Sale
                {
                    CustomerId=_saledto.CustomerId,
                    SalesDate=_saledto.SalesDate,
                    IsActive=_saledto.IsActive


                };
                await _context.Sales.AddAsync(salerecord);
                await _context.SaveChangesAsync();


                // Purchase Details 
                foreach (var _detail in _saledto.SaleDeatils)
                {
                    if (_detail.ItemId==0)
                    {
                        return "Item id not zerro";
                    }

                    var item = await _context.Items.FindAsync(_detail.ItemId);
                    if (item == null)
                    {
                        return $"Item with {_detail.ItemId} not found ";
                    }

                    if (item.NumStockQuantity<_detail.ItemQuantity)
                    {
                        return $"Insufficient stock for item: {item.ItemName}. Available stock: {item.NumStockQuantity}.";
                    }

                    item.NumStockQuantity-= _detail.ItemQuantity;
                    _context.Items.Update(item);

                    var salesDetails = new SalesDetail
                    {
                        SalesId=salerecord.SalesId,
                        ItemId = item.ItemId,
                        ItemQuantity=_detail.ItemQuantity,
                        UnitPrice=_detail.UnitPrice,
                        IsActive=salerecord.IsActive

                    };
                    await _context.SalesDetails.AddAsync(salesDetails);

                }
                await _context.SaveChangesAsync();


                return "Sales Complete Success ";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public async Task<string> GetDailyPurchaseVsSalesReport(DailyReportDto _dailypurchasedto)
        {
            throw new NotImplementedException();
        }

        public async Task<List<DailyPurchaseReportDto>> GetDailyPurchaseReport(DateTime startDate, DateTime endDate)
        {
            try
            {
                var report = await (from p in _context.Purchases
                                    join pd in _context.PurchaseDetails on p.PurchaseId equals pd.PurchaseId
                                    join i in _context.Items on pd.ItemId equals i.ItemId
                                    where  p.PurchaseDate >= startDate && p.PurchaseDate <= endDate && p.IsActive == true && pd.IsActive == true && i.IsActive == true
                                    group new { p, pd, i } by new { Date = p.PurchaseDate, i.ItemId, i.ItemName } into g
                                    select new DailyPurchaseReportDto
                                    {
                                        DailyPurchase = g.Key.Date.ToString(),
                                        ItemId = g.Key.ItemId,
                                        ItemName = g.Key.ItemName,
                                        TotalQuantity = (int)g.Sum(x => x.pd.ItemQuantity),
                                        TotalAmount = (decimal)g.Sum(x => x.pd.ItemQuantity * x.pd.UnitPrice)
                                    })
                         .OrderBy(r => r.DailyPurchase)
                         .ThenBy(r => r.ItemName)
                         .ToListAsync();

                return report;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting daily purchase report: {ex.Message}");
            }
          
        }
    }
}





