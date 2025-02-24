using WebProject.Models;

namespace WebProject.Services
{
    public class OrderDetailService
    {
        private readonly GuestModelContext _guestModelContext;
        private readonly ProductsService _productsService;

        public OrderDetailService(GuestModelContext guestModelContext, ProductsService productsService)
        {
            _guestModelContext = guestModelContext;
            _productsService = productsService;
        }
    }
}
