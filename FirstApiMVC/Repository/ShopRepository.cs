using FirstApiMVC.DbContexts;
using FirstApiMVC.DBContexts.Models;
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
        public async Task<string> CreateItem(Item item)
        {
            try
            {
                _context.Items.Add(item);
                await _context.SaveChangesAsync();
                return "Item created successfully";
            }
            catch (Exception ex)
            {
                return $"Error creating item: {ex.Message}";
            }
        }


    }
}
