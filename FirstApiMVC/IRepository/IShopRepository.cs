using FirstApiMVC.DBContexts.Models;
using FirstApiMVC.DTO;

namespace FirstApiMVC.IRepository
{
    public interface IShopRepository
    {
        
        Task<string> CreateItems(ItemListDto itemListDto);
        Task<ItemDto> UpdateItem(int Id,ItemDto item);
        Task<string>CreatePartnerType(PartnerTypeDto partnerType);
    }
}
