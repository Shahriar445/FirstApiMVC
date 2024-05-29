using FirstApiMVC.DBContexts.Models;
using FirstApiMVC.DTO;

namespace FirstApiMVC.IRepository
{
    public interface IShopRepository
    {
        
        Task<string> CreateItems(ItemListDto itemListDto);
        Task<string> UpdateItems(List<ItemDto> items);
        Task<string>CreatePartnerType(PartnerTypeDto partnerType);
        //Task<string> CreatePartner(PartnerDto partner);// working 
        Task<string> CreatePartner(string partnerName, string partnerTypeName, bool? isActive);
    }
}
