using FirstApiMVC.DbContexts;
using FirstApiMVC.DBContexts.Models;
using FirstApiMVC.DTO;
using FirstApiMVC.IRepository;
using Microsoft.EntityFrameworkCore;

namespace FirstApiMVC.Repository
{
    public class ShopRepository : IShopRepository
    {
        private readonly ShopDbContext _context;

        public ShopRepository(ShopDbContext context)
        {
            _context = context;
        }
        public async Task<string> CreateItem(ItemDto item)
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
