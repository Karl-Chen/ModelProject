using WebProject.Models;

namespace WebProject.Services
{
    public class OrderServices
    {
        private readonly GuestModelContext _guestModelContext;
        private readonly ProductsService _productsService;

        public OrderServices(GuestModelContext guestModelContext, ProductsService productsService) {
            _guestModelContext = guestModelContext;
            _productsService = productsService;
        }
    }
}
