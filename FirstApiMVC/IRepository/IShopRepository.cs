using FirstApiMVC.DBContexts.Models;
using FirstApiMVC.DTO;

namespace FirstApiMVC.IRepository
{
    public interface IShopRepository
    {
        Task<string> CreateItem(ItemDto item);
        Task<ItemDto> UpdateItem(int Id,ItemDto item);
    }
}
