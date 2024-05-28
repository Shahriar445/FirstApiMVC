using FirstApiMVC.IRepository;
using FirstApiMVC.Repository;

namespace FirstApiMVC.DependencyContainer
{
    public class DependencyInversion
    {

        public static void  RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IShopRepository, ShopRepository>();
        }
    }
}
