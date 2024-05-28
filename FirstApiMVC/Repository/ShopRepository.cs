using FirstApiMVC.DbContexts;
using FirstApiMVC.DBContexts.Models;
using FirstApiMVC.DTO;
using FirstApiMVC.IRepository;
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

        // -----------------------------Single item-------------------------------------------
        /*  public async Task<string> CreateItem(ItemDto item)
          {
              string message = "";
              try
              {

                  var data =await _context.Items.Where(e=>e.ItemId== item.ItemId).FirstOrDefaultAsync();
                  if (data!= null)
                  {
                      var e= await _context.Items.Where(e=> e.ItemName.ToLower().Trim() == item.ItemName.ToLower().Trim()).FirstOrDefaultAsync();
                      if (e != null)
                      {
                          throw new Exception("Item already Created");
                      }
                      data.ItemName = item.ItemName;
                      data.NumStockQuantity = item.NumStockQuantity;
                      data.IsActive = item.IsActive;

                      _context.Items.Update(data);
                      message = "Updated";
                  }
                  else
                  {
                      var e = await _context.Items.Where(e => e.ItemName.ToLower().Trim() == item.ItemName.ToLower().Trim()).FirstOrDefaultAsync();
                      if (e != null)
                      {
                          throw new Exception("Item already Created");
                      }
                      Item i = new Item();
                      i.ItemName = item.ItemName;
                      i.NumStockQuantity=item.NumStockQuantity;
                      i.IsActive= item.IsActive;

                      await _context.Items.AddAsync(i);
                      message = "Successful Create Item";
                  }
                  await _context.SaveChangesAsync();

              }
              catch (Exception ex)
              {
                  throw ex;

              }
              return message;
          }

        */

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



        // --------------------------------- Create Partner ---------------------------------------

        public async Task<string> CreatePartnerType(PartnerTypeDto _partnerType)
        {

            string message = "";
            try
            {

                var data = await _context.PartnerTypes.Where(e => e.PartnerTypeId == _partnerType.PartnerTypeId).FirstOrDefaultAsync();
                if (data != null)
                {
                    var e = await _context.PartnerTypes.Where(e => e.PartnerTypeName.ToLower().Trim() == _partnerType.PartnerTypeName.ToLower().Trim()).FirstOrDefaultAsync();
                    if (e != null)
                    {
                        throw new Exception("Partner Name  already Created");
                    }
                   
                    data.PartnerTypeName = _partnerType.PartnerTypeName;
                    
                    data.IsActive = _partnerType.IsActive;

                    _context.PartnerTypes.Update(data);
                    message = "Updated";
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var e = await _context.PartnerTypes.Where(e => e.PartnerTypeName.ToLower().Trim() == _partnerType.PartnerTypeName.ToLower().Trim()).FirstOrDefaultAsync();
                    if (e != null)
                    {
                        throw new Exception("Item already Created");
                    }
                    PartnerType p = new PartnerType();
                    
                    p.PartnerTypeName = _partnerType.PartnerTypeName;
            
                    p.IsActive = _partnerType.IsActive;

                    await _context.PartnerTypes.AddAsync(p);
                    message = "Successful Partner type Create";
                    await _context.SaveChangesAsync();
                }
           

            }
            catch (Exception ex)
            {
                message = ex.Message;

            }
            return message;
        }



        // ---------------------------------Update Item---------------------------------------
        public async Task<ItemDto> UpdateItem(int Id, ItemDto item)
        {
            var message = "";
            try
            {

                var data = await _context.Items.Where(e => e.ItemId == Id).FirstOrDefaultAsync();
                if (data == null)
                {
                    return null;
                }
                if (data != null)
                {
                   


                    var e = await _context.Items.Where(e => e.ItemName.ToLower().Trim() == item.ItemName.ToLower().Trim()).FirstOrDefaultAsync();
                    if (e != null)
                    {
                        throw new Exception("Item already have in db");
                    }
                    data.ItemName = item.ItemName;
                    data.NumStockQuantity = item.NumStockQuantity;
                    data.IsActive = item.IsActive;

                    _context.Items.Update(data);
                    message = "Updated";
                }

                await _context.SaveChangesAsync();

                var updatedData = await (from d in _context.Items
                                         select new ItemDto
                                         {
                                             ItemId = d.ItemId,
                                             ItemName = d.ItemName == null ? "" : d.ItemName,
                                             NumStockQuantity = d.NumStockQuantity ,
                                             
                                             IsActive = d.IsActive

                                         }).FirstOrDefaultAsync();
                return updatedData;

            }
            catch(Exception e)
            {
                throw e;
            }
        }

    }
}
