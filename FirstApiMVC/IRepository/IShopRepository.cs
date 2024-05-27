using FirstApiMVC.DBContexts.Models;

namespace FirstApiMVC.IRepository
{
    public interface IShopRepository
    {
        Task<string> CreateItem(Item item);
    }
}
