using FirstApiMVC.DBContexts.Models;
using FirstApiMVC.DTO;

namespace FirstApiMVC.IRepository
{
    public interface IShopRepository
    {
        
        Task<string> CreateItems(ItemListDto itemListDto);
        Task<string> UpdateItems(List<ItemDto> items);
       
        Task<string> CreatePartner(PartnerDto _partnerdto);
    }
}
